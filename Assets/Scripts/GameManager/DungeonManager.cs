using UnityEngine;

public class DungeonManager : GameManagerComponent
{
    [SerializeField] private GameObject gameMap;
    [field: SerializeField] public Dungeon dungeon { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        //gameMap = Instantiate(gameMap, gameManager.transform);
        if (!gameMap.TryGetComponent<Dungeon>(out Dungeon _dungeon)) Debug.LogError("Dungeon Map DOES NOT have a Dungeon script component!");
        dungeon = _dungeon;
    }
}
