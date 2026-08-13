using UnityEngine;

public class PatrolState : NPCState
{
    private int waypointIndex;
    public PatrolState(NPC npc) : base(npc)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Patrol enter");
        npc.GetAgent().SetDestination(npc.GetPath()[waypointIndex].position);
    }

    public override void OnStateExit()
    {
        Debug.Log("Patrol exit");
    }

    public override void OnStateRun()
    {
        if(npc.GetAgent().remainingDistance <= npc.GetAgent().stoppingDistance)
        {
            waypointIndex++;
            if(waypointIndex >= npc.GetPath().Length)
            {
                waypointIndex = 0;
            }
            npc.GetAgent().SetDestination(npc.GetPath()[waypointIndex].position);
        }

        Debug.Log("Patrol running");
    }
}
