using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public DungeonResources resources { get; private set; }
    public DungeonChests chests { get; private set; }
    private void Awake()
    {
        resources = GetComponentInChildren<DungeonResources>();
        if (resources == null) Debug.LogError("This Dungeon DOES NOT have a DungeonResource component!");
        chests = GetComponentInChildren<DungeonChests>();
        if (chests == null) Debug.LogError("This Dungeon DOES NOT have a DungeonChests component!");
    }
}
