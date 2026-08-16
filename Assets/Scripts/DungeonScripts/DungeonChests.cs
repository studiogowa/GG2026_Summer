using UnityEngine;

public class DungeonChests : DungeonComponent
{
    public int chestSpawnPointCount { get { return this.transform.childCount; } }
    protected override void Awake()
    {
        base.Awake();
        if (this.transform.childCount <= 0) Debug.LogWarning("This Dungeon DOES NOT have any Chest Spawn Points!");
    }
    /// <summary>
    /// Gets the coordinates for every chest spawn point in this dungeon
    /// </summary>
    /// <returns>Returns an Array of coordinates for this dungeon's chest spawn points</returns>
    public Vector3[] GetSpawnPoints()
    {
        Vector3[] ret = new Vector3[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++) ret[i] = this.transform.GetChild(i).position;
        return ret;
    }
}
