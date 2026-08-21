using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] protected NPCDefaultStates defaultState;
    [SerializeField] protected FieldOfView fieldOfView;
    [SerializeField] protected GameObject playerRef;
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshRandomPoint randomPointGenerator;
    [SerializeField] protected Transform[] patrolPathPoints;
    [SerializeField] protected int waypointIndex;
    protected NavMeshAgent agent;

    public NPCState currentState;

    [SerializeField] protected float wanderRadius;
    [SerializeField] protected float rotationSpeed;
    public Vector3 nextTarget { get; private set; }
    protected Vector3 currentDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //NavMesh Setup
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        NavMeshHit hit;

        bool found = NavMesh.SamplePosition(
            transform.position,
            out hit,
            1f,
            NavMesh.AllAreas
        );

        //FOV Setup
        fieldOfView.SetRotationSpeed(rotationSpeed);
        fieldOfView.onPlayerInRange += StartChase;
        fieldOfView.onPlayerLost += EndChase;

        //Chase Player Setup
        playerRef = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log($"NPC position: {transform.position}");
        //Debug.Log($"Nearest NavMesh: {hit.position}");
        //Debug.Log($"Distance: {Vector3.Distance(transform.position, hit.position)}");

        //ChangeState(new PatrolState(this));
        ChangeState(new WanderState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateRun();
        RotateFOV();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void RotateFOV()
    {
        //Vector3 aimDir = (nextTarget - transform.position).normalized;
        Vector3 aimDir = agent.velocity.normalized;
        fieldOfView.SetAimDirection(aimDir);
    }

    private void StartChase()
    {
        ChangeState(new ChaseState(this, playerRef.transform));
    }

    private void EndChase()
    {
        ReturnToDefaultState();
    }

    public void UpdateTarget(Vector3 playerLocation)
    {
        nextTarget = playerLocation;
    }


    public void ChangeState(NPCState state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        currentState.OnStateEnter();
    }

    public void ReturnToDefaultState()
    {
        switch (defaultState)
        {
            case NPCDefaultStates.patrol:
                ChangeState(new  PatrolState(this)); 
                break;

            case NPCDefaultStates.wander:
                ChangeState(new WanderState(this));
                break;
        }
    }


    public bool TryGetRandomPoint()
    {
        if (randomPointGenerator.TryGetRandomPoint(transform.position, wanderRadius, out Vector3 newTarget))
        {
            nextTarget = new Vector3(newTarget.x, newTarget.y, 0f);
            return true;
        }

        return false;
    }
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    public Transform[] GetPath()
    {
        return patrolPathPoints;
    }

}

public enum NPCDefaultStates
{
    patrol,
    wander
}

