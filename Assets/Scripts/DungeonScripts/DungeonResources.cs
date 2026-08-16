using UnityEngine;
using System.Collections.Generic;
public class DungeonResources : DungeonComponent
{
    public int resourceSpawnRectCount { get { return resourceSpawnRects.Count; } }
    [field: SerializeField] public List<Rect> resourceSpawnRects { get; private set; }

    /// <summary>
    /// Randomly gets [spawnCount] resource node spawn coordinates in this Dungeon
    /// </summary>
    /// <param name="spawnCount">Number of spawn points to return</param>
    /// <returns>Returns an array of Vector3 coordinates</returns>
    public Vector3[] GetSpawnPoints(int spawnCount)
    {
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
