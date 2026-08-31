using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ItemPool", menuName = "Scriptable Objects/ItemPool")]
public class ItemPool : ScriptableObject
{
    public List<ItemPoolItem> itemPoolItems;
}

[System.Serializable]
public struct ItemPoolItem
{
    public Item item;

    [Tooltip("The minimum amount spawnable if the Item is Stackable")]
    [Range(1, 20)] public int stackableMinAmount;
    [Tooltip("The maxmimum amount spawnable if the Item is Stackable")]
    [Range(1, 20)] public int stackableMaxAmount;
}