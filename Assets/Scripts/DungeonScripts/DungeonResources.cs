using UnityEngine;
using System.Collections.Generic;
public class DungeonResources : DungeonComponent
{
    public int resourceSpawnRectCount { get { return resourceSpawnRects.Count; } }
    private List<Rect> resourceSpawnRects;

    protected override void Awake()
    {
        base.Awake();
        CompileSpawnRects();
        if (this.transform.childCount <= 0) Debug.LogError("This Dungeon DOES NOT have any Resource Spawning Zones!");
    }
    /// <summary>
    /// Iterates through all children of this GameObject for AreaRect components and records them to resourceSpawnRects
    /// </summary>
    private void CompileSpawnRects()
    {
        resourceSpawnRects = new List<Rect>();
        foreach (Transform childTransform in this.transform)
        {
            if (childTransform.TryGetComponent<AreaRect>(out AreaRect currAreaRect)) resourceSpawnRects.Add(currAreaRect.areaRect);
            else Debug.LogWarning($"{childTransform.name} DOES NOT have an AreaRect component! Skipping!");
        }
    }
    /// <summary>
    /// Randomly gets [spawnCount] resource node spawn coordinates in this Dungeon
    /// </summary>
    /// <param name="spawnCount">Number of spawn points to return</param>
    /// <returns>Returns an array of Vector3 coordinates</returns>
    public Vector3[] GetSpawnPoints(int spawnCount)
    {
        CompileSpawnRects();

        Vector3[] ret = new Vector3[spawnCount];
        for (int i = 0; i < resourceSpawnRects.Count; i++)
        {
            // Pick a point within the spawn Rect
            Vector3 spawnPoint = new Vector3(
                Random.Range(resourceSpawnRects[i].xMin, resourceSpawnRects[i].xMax),
                Random.Range(resourceSpawnRects[i].yMin, resourceSpawnRects[i].yMax),
                0.0f
            );
            ret[i] = spawnPoint;
        }
        ArrayHelpers.ShuffleArray(ret);

        return ret[0..spawnCount];
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    foreach (Rect currRect in resourceSpawnRects)
    //    {
    //        Vector3 center = new Vector3(currRect.center.x, currRect.center.y, 0.0f);
    //        Vector3 size = new Vector3(currRect.width, currRect.height, 0.01f);

    //        Gizmos.DrawWireCube(center, size);
    //    }
    //}
}
