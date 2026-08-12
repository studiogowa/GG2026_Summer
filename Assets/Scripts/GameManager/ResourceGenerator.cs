using UnityEngine;

public class ResourceGenerator : GameManagerComponent
{
    private void OnEnable()
    {
        gameManager.gameEvents.duskStarts += GenerateResources;
        gameManager.gameEvents.dawnStarts += GenerateChests;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.duskStarts -= GenerateResources;
        gameManager.gameEvents.dawnStarts -= GenerateChests;
    }
    /// <summary>
    /// Generates dead bodies with a random amount of lootable items on them
    /// </summary>
    private void GenerateResources()
    {  

    }
    /// <summary>
    /// Spawn chests and assigns their value level
    /// </summary>
    private void GenerateChests()
    {

    }
}
