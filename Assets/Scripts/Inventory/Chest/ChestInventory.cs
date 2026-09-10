using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : Inventory
{
    [SerializeField, Range(1, 10)] public int amountTarget = 1;
    [SerializeField, Range(1, 20)] public int valueTarget = 1;
    
    public void SetValueTarget(int target)
    {
        valueTarget = target;
    }
    public void SetAmountTarget(int target)
    {
        amountTarget = target;
    }
    /// <summary>
    /// Calculates the quality of this chest
    /// </summary>
    /// <returns>A percentage of this chest's quality</returns>
    public float CalculateChestQuality(int strategy)
    {
        switch (strategy)
        {
            case 0:
                return CalculateAmountQuality();
            case 1:
                return CalculateValueQuality();
            default:
                Debug.LogError("INVALID QUALITY CALCULATION STRATEGY");
                return 0.0f;
        }
    }
    private float CalculateAmountQuality()
    {
        float ret;
        float percentToTarget = Mathf.Clamp(Mathf.Abs(amountTarget - items.Count) / (float)amountTarget, 0.0f, 1.0f);
        // Overfilling has 50% less penalty to score
        if (items.Count > amountTarget) ret = 1 - (percentToTarget * 0.5f);
        // Underfilling suffers full penalty to score
        else ret = 1 - percentToTarget;

        return ret * 100;
    }
    private float CalculateValueQuality()
    {
        int valueSum = 0;
        float ret;
        foreach (Item currItem in items) valueSum += (currItem.amount * currItem.value);

        float percentToTarget = Mathf.Clamp(Mathf.Abs(valueTarget - valueSum) / (float)valueTarget, 0.0f, 1.0f);
        // Overfilling has 50% less penalty to score
        if (valueSum > valueTarget) ret = 1 - (percentToTarget * 0.5f);
        // Underfilling suffers full penalty to score
        else ret = 1 - percentToTarget;

        return ret * 100;
    }
    public bool IsEmpty()
    {
        return items.Count <= 0;
    }
}
