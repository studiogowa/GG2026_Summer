using UnityEngine;
using System.Collections.Generic;
public class ResourceGenerator : GameManagerComponent
{
    public GameObject resourceCollection { get; private set; }

    [SerializeField, Range(1, 20)] private int resourceSpawnCount;
    protected override void Awake()
    {
        base.Awake();
        if (resourceCollection == null)
        {
            resourceCollection = new GameObject("RESOURCE COLLECTION");
            resourceCollection.transform.parent = this.transform;
        }
    }
    private void OnEnable()
    {
        gameManager.gameEvents.duskStarts += DuskSetup;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.duskStarts -= DuskSetup;
    }
    private void DuskSetup()
    {
        ClearResources();
        GenerateResources();
    }
    private void ClearResources()
    {
        for (int i = resourceCollection.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(resourceCollection.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// Generates dead bodies with a random amount of lootable items on them
    /// </summary>
    private void GenerateResources()
    {
        if (gameManager.dungeonManager.resourceSpawnPointCount <= 0)
        {
            Debug.LogWarning("No Resource Spawn Points were set! No Resources will spawn!");
            return;
        }
        // Clamp resource spawn count if too many resources will spawn
        if (resourceSpawnCount > gameManager.dungeonManager.resourceSpawnPointCount)
        {
            Debug.LogWarning("More Resources were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {gameManager.dungeonManager.resourceSpawnPointCount}!");
            resourceSpawnCount = gameManager.dungeonManager.resourceSpawnPointCount;
        }
        // Choose spawn locations
        Vector3[] spawnPoints = gameManager.dungeonManager.GetResourceSpawns(resourceSpawnCount);

        // Spawn Resources
        foreach (Vector3 spawnCoords in spawnPoints)
        {
            GameObject chest = new GameObject("Resource");
            chest.transform.parent = resourceCollection.transform;
            chest.transform.position = spawnCoords;
        }
    }
}