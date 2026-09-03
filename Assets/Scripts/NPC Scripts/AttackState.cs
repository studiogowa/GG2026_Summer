using UnityEngine;

public class AttackState : NPCState
{
    public AttackState(NPC npc) : base(npc)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("start attack animation");
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {
        Debug.Log("Check when attack has ended then exit state.");
        npc.ReturnToDefaultState();
    }

}
