using UnityEngine;

public class ChestSpawner : GameManagerComponent
{
    public GameObject chestCollection { get; private set; }
    [SerializeField, Range(1, 12)] private int chestSpawnCount = 6;

    protected override void Awake()
    {
        base.Awake();
        if (chestCollection == null)
        {
            chestCollection = new GameObject("CHEST COLLECTION");
            chestCollection.transform.parent = this.transform;
        }
    }
    private void OnEnable()
    {
        gameManager.gameEvents.duskStarts += SetupChests;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.duskStarts -= SetupChests;
    }
    private void SetupChests()
    {
        ClearChests();
        GenerateChests();
    }
    /// <summary>
    /// Destroys every chest under chestCollection
    /// </summary>
    private void ClearChests()
    {
        for (int i = chestCollection.transform.childCount - 1; i >= 0; i --)
        {
            Destroy(chestCollection.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// Spawn chests and assigns their value level
    /// </summary>
    private void GenerateChests()
    {
        if (gameManager.dungeonManager.dungeon.chests.chestSpawnPointCount <= 0)
        {
            Debug.LogWarning("No Chests Spawn Points were set! No Chests will spawn!");
            return;
        }
        // Clamp chest spawn count if too many chests will spawn
        if (chestSpawnCount > gameManager.dungeonManager.dungeon.chests.chestSpawnPointCount)
        {
            Debug.LogWarning("More chests were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {gameManager.dungeonManager.dungeon.chests.chestSpawnPointCount}!");
            chestSpawnCount = gameManager.dungeonManager.dungeon.chests.chestSpawnPointCount;
        }
        // Get spawn locations
        Vector3[] spawnPoints = gameManager.dungeonManager.dungeon.chests.GetSpawnPoints(chestSpawnCount);

        // Spawn chests
        foreach (Vector3 spawnCoords in spawnPoints)
        {
            GameObject chest = new GameObject("Chest");
            chest.transform.parent = chestCollection.transform;
            chest.transform.position = spawnCoords;
        }
    }
}
