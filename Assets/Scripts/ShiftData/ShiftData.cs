using UnityEngine;

[CreateAssetMenu(fileName = "ShiftData", menuName = "Scriptable Objects/ShiftData")]
public class ShiftData : ScriptableObject
{
    [Header("Shift Interactables Variables")]
    [Range(1, 15)] public int chestCount = 5;
    public int[] chestValueTargets;
    [Space(10)]
    [Range(1, 15)] public int lootablesCount = 5;
    public int[] lootablesItemCount;
    [Space(10)]
    [Range(1, 15)] public int explorerCount = 5;

    [Header("Shift Timing Variables (in seconds)")]
    [Range(0.0f, 30.0f)] public float preGameTime = 5.0f;
    [Range(0.0f, 180.0f)] public float duskRoundTime = 30.0f;
    [Range(0.0f, 30.0f)] public float preDawnTime = 5.0f;
    [Range(0.0f, 180.0f)] public float dawnRoundTime = 60.0f;
    [Range(0.0f, 30.0f)] public float preDayTime = 5.0f;
    [Range(0.0f, 180.0f)] public float dayRoundTime = 30.0f;
    [Range(0.0f, 30.0f)] public float dayEndPause = 5.0f;
}
