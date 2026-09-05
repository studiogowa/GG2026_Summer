using UnityEngine;

public class ExplorerSpawner : GameManagerComponent
{
    [SerializeField] private GameObject explorerPrefab;
    private GameObject explorerCollection;
    [field: SerializeField, Range(1, 12)] public int explorerSpawnCount { get; private set; } = 6;
    protected override void Awake()
    {
        base.Awake();
        if (explorerPrefab == null) Debug.LogError("Reference the Explorer Prefab to the ExplorerSpawner Component!");
        if (explorerCollection == null)
        {
            explorerCollection = new GameObject("EXPLORER COLLECTION");
            explorerCollection.transform.parent = this.transform;
        }
    }
    protected void OnEnable()
    {
        gameManager.gameEvents.dawnStarts += SpawnExplorers;
    }
    protected void OnDisable()
    {
        gameManager.gameEvents.dawnStarts -= SpawnExplorers;
    }
    /// <summary>
    /// Spawns Explorers to hunt down the player
    /// </summary>
    private void SpawnExplorers()
    {
        Vector3[] offscreenSpawnPoints = gameManager.dungeonManager.GetExplorerSpawns();

        for (int i = 0; i < explorerSpawnCount; i ++)
        {
            Vector3 spawnPoint = offscreenSpawnPoints[Random.Range(0, offscreenSpawnPoints.Length)];
            GameObject currExplorer = new GameObject("EXPLORER PLACEHOLDER");
            currExplorer.transform.position = spawnPoint;
            currExplorer.transform.parent = explorerCollection.transform;
        }
    }
}