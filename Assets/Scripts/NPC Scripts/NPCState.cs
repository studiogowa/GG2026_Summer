using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NPCState 
{
    protected NPC npc;

    public NPCState(NPC npc)
    {
        this.npc = npc;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateRun();
    public abstract void OnStateExit();
}
