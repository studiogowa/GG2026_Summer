using UnityEngine;

public class DungeonManager : GameManagerComponent
{
    [SerializeField] private GameObject dungeonGameObject;
    private Dungeon dungeon;
    protected override void Awake()
    {
        base.Awake();
        //gameMap = Instantiate(gameMap, gameManager.transform);
        if (!dungeonGameObject.TryGetComponent<Dungeon>(out dungeon)) Debug.LogError("Dungeon Map DOES NOT have a Dungeon script component!");
    }

    public Vector3 GetPlayerSpawn()
    {
        return dungeon.playerSpawn.GetPlayerSpawn();
    }

    public int chestSpawnPointCount { get { return dungeon.chests.chestSpawnPointCount; } }
    /// <summary>
    /// Randomly get [chestCount] spawn coordinates for chests in this Dungeon
    /// </summary>
    /// <param name="chestCount">Number of coordinates to get</param>
    /// <returns>Returns an Array of Vector 3 Coordinates</returns>
    public Vector3[] GetChestSpawns(int chestCount)
    {
        if (dungeon == null) return new Vector3[0];
        return dungeon.chests.GetSpawnPoints(chestCount);
    }

    public int resourceSpawnPointCount { get { return dungeon.resources.resourceSpawnRectCount; } }
    /// <summary>
    /// Randomly get [resourceCount] spawn coordinates for resource nodes in this Dungeon
    /// </summary>
    /// <param name="resourceCount">Number of coordinates to get</param>
    /// <returns>Returns an Array of Vector 3 Coordinates</returns>
    public Vector3[] GetResourceSpawns(int resourceCount)
    {
        if (dungeon == null) return new Vector3[0];
        return dungeon.resources.GetSpawnPoints(resourceCount);
    }

    public bool IsInExtractionArea(Vector2 point)
    {
        return dungeon.extraction.IsInExtractionArea(point);
    }
}
