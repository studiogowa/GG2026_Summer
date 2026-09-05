using UnityEngine;

[CreateAssetMenu(fileName = "ShiftData", menuName = "Scriptable Objects/ShiftData")]
public class ShiftData : ScriptableObject
{
    [Header("Shift Interactables Variables")]
    public ShiftChestData[] chests;
    public int chestCount { get { return chests.Length; } }
    [Space(10)]
    public ShiftLootablesData[] lootables;
    public int lootablesCount { get { return lootables.Length; } }
    [Space(10)]
    [Range(1, 15)] public int explorerCount = 5;
    [Space(10)]
    [Range(0, 100)] public int passingGrade = 50;

    [Header("Shift Timing Variables (in seconds)")]
    [Range(0.0f, 30.0f)] public float preGameTime = 5.0f;
    [Range(0.0f, 180.0f)] public float duskRoundTime = 30.0f;
    [Range(0.0f, 30.0f)] public float preDawnTime = 5.0f;
    [Range(0.0f, 180.0f)] public float dawnRoundTime = 60.0f;
    [Range(0.0f, 30.0f)] public float preDayTime = 5.0f;
    [Range(0.0f, 180.0f)] public float dayRoundTime = 30.0f;
    [Range(0.0f, 30.0f)] public float dayEndPause = 5.0f;
}

[System.Serializable]
public struct ShiftChestData
{
    [Range(0, 6)] public int chestAmountTarget;
    [Range(0, 12)] public int chestValueTarget;
}

[System.Serializable]
public struct ShiftLootablesData
{
    [Range(1, 8)] public int itemCount;
}
