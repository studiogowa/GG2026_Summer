using UnityEngine;
using System.Collections.Generic;
public class DungeonExtraction : DungeonComponent
{
    private List<Rect> extractionAreaRects;
    protected override void Awake()
    {
        base.Awake();
        extractionAreaRects = new List<Rect>();
        if (transform.childCount <= 0) Debug.LogError("This Dungeon DOES NOT have ANY Extraction Area Colliders!");
    }
    /// <summary>
    /// Iterates through all children of this GameObject for AreaRect components and records them to extractionAreaRects
    /// </summary>
    private void CompileExtractionRects()
    {
        extractionAreaRects = new List<Rect>();
        foreach (Transform childTransform in this.transform)
        {
            if (childTransform.TryGetComponent<AreaRect>(out AreaRect currAreaRect)) extractionAreaRects.Add(currAreaRect.areaRect);
            else Debug.LogWarning($"{childTransform.name} DOES NOT have an AreaRect component! Skipping!");
        }
    }
    /// <summary>
    /// Check whether a point is in any Extraction Area
    /// </summary>
    /// <param name="point">The point to check</param>
    /// <returns>True if it is in any Extraction Area, false otherwise</returns>
    public bool IsInExtractionArea(Vector2 point)
    {
        CompileExtractionRects();
        foreach (Rect currRect in extractionAreaRects)
        {
            if (currRect.Contains(point)) return true;
        }

        return false;
    }
}
