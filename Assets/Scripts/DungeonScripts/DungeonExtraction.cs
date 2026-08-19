using UnityEngine;
using System.Collections.Generic;
public class DungeonExtraction : DungeonComponent
{
    [SerializeField] private List<Collider2D> extractionAreas;
    protected override void Awake()
    {
        base.Awake();

        GetComponents<Collider2D>(extractionAreas);
        if (extractionAreas.Count <= 0) Debug.LogError("This Dungeon DOES NOT have ANY Extraction Area Colliders!");
    }

    public bool IsInExtractionArea(Collider2D collider)
    {
        foreach (Collider2D currCollider in extractionAreas)
        {
            if (currCollider.IsTouching(collider)) return true;
        }

        return false;
    }
}
