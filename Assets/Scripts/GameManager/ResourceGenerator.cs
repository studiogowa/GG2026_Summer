using UnityEngine;
using System.Collections.Generic;
public class ResourceGenerator : GameManagerComponent
{
    [SerializeField] private List<Rect> resourceSpawnRects;
    [SerializeField] private GameObject resourceCollection;

    [SerializeField, Range(1, 20)] private int resourceSpawnCount;
    protected override void Awake()
    {
        base.Awake();
        if (resourceCollection == null)
        {
            Debug.LogWarning("No resourcesCollection has been set!");
            resourceCollection = new GameObject("DEFAULT RESOURCE COLLECTION");
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
        if (resourceSpawnRects.Count <= 0)
        {
            Debug.LogWarning("No Resource Spawn Points were set! No Resources will spawn!");
            return;
        }
        // Clamp resource spawn count if too many resources will spawn
        if (resourceSpawnCount > resourceSpawnRects.Count)
        {
            Debug.LogWarning("More Resources were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {resourceSpawnRects.Count}!");
            resourceSpawnCount = resourceSpawnRects.Count;
        }
        // Choose spawn locations
        Vector3[] spawnPoints = new Vector3[resourceSpawnRects.Count];
        for (int i = 0; i < resourceSpawnRects.Count; i++)
        {
            Vector3 spawnPoint = new Vector3(
                Random.Range(resourceSpawnRects[i].xMin, resourceSpawnRects[i].xMax),
                Random.Range(resourceSpawnRects[i].yMin, resourceSpawnRects[i].yMax),
                0.0f
            );
            spawnPoints[i] = spawnPoint;
        }
        ArrayHelpers.ShuffleArray(spawnPoints);

        // Spawn Resources
        for (int i = 0; i < resourceSpawnCount; i++)
        {
            GameObject chest = new GameObject("Resource");
            chest.transform.parent = resourceCollection.transform;
            chest.transform.position = spawnPoints[i];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Rect currRect in resourceSpawnRects)
        {
            Vector3 center = new Vector3(currRect.center.x, currRect.center.y, 0.0f);
            Vector3 size = new Vector3(currRect.width, currRect.height, 0.01f);

            Gizmos.DrawWireCube(center, size);
        }
    }
}