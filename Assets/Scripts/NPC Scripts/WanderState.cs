using UnityEngine;

public class WanderState : NPCState
{
    public WanderState(NPC npc) : base(npc)
    {
    }

    public override void OnStateEnter()
    {
        //Debug.Log("wander enter");
    }

    public override void OnStateExit()
    {
        Debug.Log("wander exit");
    }

    public override void OnStateRun()
    {
        if(!npc.GetAgent().pathPending &&
            npc.GetAgent().remainingDistance <= npc.GetAgent().stoppingDistance ||
            !npc.GetAgent().hasPath)
        {
           if (npc.TryGetRandomPoint())
            {
                npc.GetAgent().SetDestination(npc.newTarget);
            }
        }
    }
}
