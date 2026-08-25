using UnityEngine;

public class PlayerManager : GameManagerComponent
{
    private void OnEnable()
    {
        gameManager.gameEvents.preGameStarts += SpawnPlayer;
        gameManager.gameEvents.preGameStarts += DisablePlayerMovement;

        gameManager.gameEvents.duskStarts += EnablePlayerMovement;

        gameManager.gameEvents.duskEnds += SpawnPlayer;

        gameManager.gameEvents.dayEnds += DisablePlayerMovement;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.preGameStarts -= SpawnPlayer;
        gameManager.gameEvents.preGameStarts -= DisablePlayerMovement;

        gameManager.gameEvents.duskStarts -= EnablePlayerMovement;

        gameManager.gameEvents.duskEnds -= SpawnPlayer;

        gameManager.gameEvents.dayEnds -= DisablePlayerMovement;
    }
    private void SpawnPlayer()
    {
        gameManager.player.transform.position = gameManager.dungeonManager.GetPlayerSpawn();
        CameraScript.instance.transform.position = new Vector3(
            gameManager.player.transform.position.x,
            gameManager.player.transform.position.y, 
            CameraScript.instance.transform.position.z
        );
    }
    private void EnablePlayerMovement()
    {

    }
    private void DisablePlayerMovement() 
    { 

    }
}
