using UnityEngine;
using System.Collections.Generic;
public class ResourceGenerator : GameManagerComponent
{
    [SerializeField] private List<Rect> lootSpawnRects;
 
    protected override void Awake()
    {
        base.Awake();
        lootSpawnRects = new List<Rect>();
    }
    private void OnEnable()
    {
        gameManager.gameEvents.duskStarts += GenerateResources;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.duskStarts -= GenerateResources;
    }
    /// <summary>
    /// Generates dead bodies with a random amount of lootable items on them
    /// </summary>
    private void GenerateResources()
    {  

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        foreach (Rect currRect in lootSpawnRects)
        {
            Vector3 center = new Vector3(currRect.center.x, currRect.center.y, 0.0f);
            Vector3 size = new Vector3(currRect.width, currRect.height, 0.01f);

            Gizmos.DrawWireCube(center, size);
        }
    }
}