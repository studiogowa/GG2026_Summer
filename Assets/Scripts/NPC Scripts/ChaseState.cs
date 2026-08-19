using UnityEngine;

public class ChaseState : NPCState
{
    private Transform target;
    public ChaseState(NPC npc, Transform target) : base(npc)
    {
        this.target = target;
    }

    public override void OnStateEnter()
    {
        //change speed?
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        npc.GetAgent().SetDestination(target.position);
        npc.UpdateTarget(target.position);
    }
}
