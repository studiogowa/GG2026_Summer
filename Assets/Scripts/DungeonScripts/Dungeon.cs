using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public DungeonResources resources { get; private set; }
    public DungeonPlayerSpawn playerSpawn { get; private set; }
    public DungeonChests chests { get; private set; }
    public DungeonExtraction extraction { get; private set; }
    private void Awake()
    {
        resources = GetComponentInChildren<DungeonResources>();
        if (resources == null) Debug.LogError("This Dungeon DOES NOT have a DungeonResource component!");
        playerSpawn = GetComponentInChildren<DungeonPlayerSpawn>();
        if (playerSpawn == null) Debug.LogError("This Dungeon DOES NOT have a DungeonPlayerSpawn component!");
        chests = GetComponentInChildren<DungeonChests>();
        if (chests == null) Debug.LogError("This Dungeon DOES NOT have a DungeonChests component!");
        extraction = GetComponentInChildren<DungeonExtraction>();
        if (extraction == null) Debug.LogError("This Dungeon DOES NOT have a DungeonExtraction component!");
    }
}
