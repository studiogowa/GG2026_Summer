using UnityEngine;

public class ChestSpawner : GameManagerComponent
{
    [SerializeField] private GameObject chestSpawnPointCollection;
    public GameObject chestCollection { get; private set; }
    [SerializeField, Range(1, 12)] private int chestSpawnCount = 6;

    protected override void Awake()
    {
        base.Awake();
        if (chestSpawnPointCollection == null)
        {
            Debug.LogWarning("No ChestSpawnPointCollection has been set!");
            chestSpawnPointCollection = new GameObject("DEFAULT CHEST SPAWN POINT COLLECTION");
            chestSpawnPointCollection.transform.parent = this.transform;
        }
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
    // Destroys every chest under chestCollection
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
        if (chestSpawnPointCollection.transform.childCount <= 0)
        {
            Debug.LogWarning("No Chests Spawn Points were set! No Chests will spawn!");
            return;
        }
        // Clamp chest spawn count if too many chests will spawn
        if (chestSpawnCount > chestSpawnPointCollection.transform.childCount)
        {
            Debug.LogWarning("More chests were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {chestSpawnPointCollection.transform.childCount}!");
            chestSpawnCount = chestSpawnPointCollection.transform.childCount;
        }
        // Choose spawn locations
        Vector3[] spawnPoints = new Vector3[chestSpawnPointCollection.transform.childCount];
        for (int i = 0; i < chestSpawnPointCollection.transform.childCount; i++) spawnPoints[i] = chestSpawnPointCollection.transform.GetChild(i).transform.position;
        ArrayHelpers.ShuffleArray(spawnPoints);

        // Spawn chests
        for (int i = 0; i < chestSpawnCount; i ++)
        {
            GameObject chest = new GameObject("Chest");
            chest.transform.parent = chestCollection.transform;
            chest.transform.position = spawnPoints[i];
        }
    }
}
