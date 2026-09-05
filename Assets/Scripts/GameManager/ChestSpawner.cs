using UnityEngine;

public class ChestSpawner : GameManagerComponent
{
    [SerializeField] private GameObject chestPrefab;
    public GameObject chestCollection { get; private set; }
    public int chestsSpawned { get { return chestCollection.transform.childCount; } }
    public int chestSpawnCount { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (chestPrefab == null) Debug.LogError("Reference the Chest Prefab to the ChestSpawner Component!");
        if (chestCollection == null)
        {
            chestCollection = new GameObject("CHEST COLLECTION");
            chestCollection.transform.parent = this.transform;
        }
    }
    private void OnEnable()
    {
        gameManager.gameEvents.preGameStarts += SetupChests;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.preGameStarts -= SetupChests;
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
        if (gameManager.dungeonManager.chestSpawnPointCount <= 0)
        {
            Debug.LogWarning("No Chests Spawn Points were set! No Chests will spawn!");
            return;
        }
        chestSpawnCount = gameManager.currShiftData.chestCount;
        // Clamp chest spawn count if too many chests will spawn
        if (chestSpawnCount > gameManager.dungeonManager.chestSpawnPointCount)
        {
            Debug.LogWarning("More chests were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {gameManager.dungeonManager.chestSpawnPointCount}!");
            chestSpawnCount = gameManager.dungeonManager.chestSpawnPointCount;
        }
        // Get spawn locations
        Vector3[] spawnPoints = gameManager.dungeonManager.GetChestSpawns(chestSpawnCount);

        // Spawn chests
        int currChestCount = 0;
        foreach (Vector3 spawnCoords in spawnPoints)
        {
            GameObject chest = Instantiate(chestPrefab);
            chest.transform.parent = chestCollection.transform;
            chest.transform.position = spawnCoords;

            // Set chest value target
            if (chest.TryGetComponent<ChestInventory>(out ChestInventory currChestInventory))
            {
                currChestInventory.SetValueTarget(gameManager.currShiftData.chests[currChestCount].chestValueTarget);
                currChestInventory.SetAmountTarget(gameManager.currShiftData.chests[currChestCount].chestAmountTarget);
            }

            currChestCount++;
        }
    }
}
