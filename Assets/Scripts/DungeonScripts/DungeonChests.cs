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
    /// Randomly gets [spawnCount] chest spawn coordinates in this dungeon
    /// </summary>
    /// <param name="spawnCount">Number of spawn points to return</param>
    /// <returns>Returns an array of Vector3 coordinates</returns>
    public Vector3[] GetSpawnPoints(int spawnCount)
    {
        Vector3[] ret = new Vector3[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++) ret[i] = this.transform.GetChild(i).position;
        ArrayHelpers.ShuffleArray(ret);

        return ret[0..spawnCount];
    }
}
