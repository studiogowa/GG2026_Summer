using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] Transform target;
    [SerializeField] NavMeshRandomPoint randomPointGenerator;
    [SerializeField] private Transform[] patrolPathPoints;
    [SerializeField] private int waypointIndex;
    private NavMeshAgent agent;

    public NPCState currentState;

    [SerializeField] private float wanderRadius;
    [SerializeField] private float rotationSpeed;
    public Vector3 nextTarget { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Vector3 aimDir = (nextTarget - transform.position).normalized;
        fieldOfView.SetAimDirection(aimDir);
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
