using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : Inventory
{
    [SerializeField, Range(1, 20)] public int valueTarget = 1;
    public void SetValueTarget(int target)
    {
        valueTarget = target;
    }
    /// <summary>
    /// Calculates the quality of this chest
    /// </summary>
    /// <returns>A percentage of this chest's quality</returns>
    public float CalculateChestQuality()
    {
        int valueSum = 0;
        foreach (Item currItem in items) valueSum += (currItem.amount * currItem.value);

        float differenceRatio = Mathf.Clamp(Mathf.Abs(valueTarget - valueSum) / (float)valueTarget, 0.0f, 100.0f);
        float ret = 1 - differenceRatio;

        Debug.Log($"This chest has {valueSum} with a rating of {ret}");
        return ret*100;
    }
    public bool IsEmpty()
    {
        return items.Count <= 0;
    }
}
