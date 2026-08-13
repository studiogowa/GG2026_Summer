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

    public float wanderRadius {  get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ChangeState(new PatrolState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateRun();
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

    //public NavMeshRandomPoint GetRandomPoint ()
    //{

    //}
    public Transform[] GetPath()
    {
        return patrolPathPoints;
    }
}
