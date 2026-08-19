using UnityEngine;

public class DungeonPlayerSpawn : DungeonComponent
{
    public Vector3 GetPlayerSpawn()
    {
        return this.transform.position;
    }
}
