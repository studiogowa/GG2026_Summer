using UnityEngine;

public class ExplorerSpawner : GameManagerComponent
{
    protected void OnEnable()
    {
        gameManager.gameEvents.dawnStarts += SpawnExplorers;
    }
    protected void OnDisable()
    {
        gameManager.gameEvents.dawnStarts -= SpawnExplorers;
    }
    /// <summary>
    /// Spawns Explorers to hunt down the player
    /// </summary>
    private void SpawnExplorers()
    {

    }
}