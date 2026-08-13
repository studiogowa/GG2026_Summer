using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NavMeshRandomPoint randomPointGenerator;
    [SerializeField] private Transform[] patrolPathPoints;
    [SerializeField] private int waypointIndex;
    private NavMeshAgent agent;

    public NPCState currentState;

    [SerializeField] private float wanderRadius;
    public Vector3 newTarget { get; private set; }

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

        Debug.Log($"NPC position: {transform.position}");
        Debug.Log($"Nearest NavMesh: {hit.position}");
        Debug.Log($"Distance: {Vector3.Distance(transform.position, hit.position)}");

        //ChangeState(new PatrolState(this));
        ChangeState(new WanderState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateRun();
        //if (transform.position.z != 0)
        //{
        //    Debug.Log($"NPC Z CHANGED TO: {transform.position.z}");
        //}
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
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

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public bool TryGetRandomPoint()
    {
        if (randomPointGenerator.TryGetRandomPoint(transform.position, wanderRadius, out Vector3 newTarget))
        {
            this.newTarget = newTarget;
            return true;
        }

        return false;
    }
    public Transform[] GetPath()
    {
        return patrolPathPoints;
    }
}
