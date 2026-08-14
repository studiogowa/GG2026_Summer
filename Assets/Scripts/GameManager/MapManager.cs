using UnityEngine;

public class MapManager : GameManagerComponent
{
    [SerializeField] private GameObject gameMap;
    protected override void Awake()
    {
        base.Awake();
        gameMap = Instantiate(gameMap, gameManager.transform);
    }
}
