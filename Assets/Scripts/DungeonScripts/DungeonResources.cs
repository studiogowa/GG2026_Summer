using UnityEngine;
using System.Collections.Generic;
public class DungeonResources : DungeonComponent
{
    [field: SerializeField] public List<Rect> resourceSpawnRects { get; private set; }

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
