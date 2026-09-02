using UnityEngine;
using System.Collections.Generic;
public class DungeonExplorer : DungeonComponent
{
    protected override void Awake()
    {
        base.Awake();
        if (this.transform.childCount <= 0) Debug.LogWarning("This Dungeon DOES NOT have any Explorer Spawn Points!");
    }
    /// <summary>
    /// Gets all OFFSCREEN Explorer spawn coordinates in this dungeon
    /// </summary>
    /// <returns>Returns an array of Vector3 coordinates</returns>
    public Vector3[] GetSpawnPoints()
    {
        List<Vector3> ret = new List<Vector3>();
        foreach (Transform currTransform in this.transform)
        {
            // Add point if it's outside of Main Camera view
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(currTransform.position);

            bool isOutXBounds = viewportPoint.x < 0.0f && viewportPoint.x > 1.0f;
            bool isOutYBounds = viewportPoint.y < 0.0f && viewportPoint.y > 1.0f;

            if (isOutXBounds || isOutYBounds)
            {
                ret.Add(currTransform.position);
            }
        }
        // Return all spawn points if all the spawns are somehow in the Camera Bounds
        if (ret.Count <= 0)
        {
            foreach (Transform currTransform in this.transform) ret.Add(currTransform.position);
        }
        
        return ret.ToArray();
    }
}
