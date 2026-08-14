using UnityEngine;

public class ChestSpawner : GameManagerComponent
{
    [SerializeField] private GameObject chestSpawnPointCollection;
    [SerializeField, Range(0, 12)] private int chestSpawnCount = 6;

    protected override void Awake()
    {
        base.Awake();
        if (chestSpawnPointCollection == null)
        {
            Debug.LogWarning("No ChestSpawnPointCollection has been set!");
            chestSpawnPointCollection = new GameObject("DEFAULT CHEST SPAWN POINT COLLECTION");
            chestSpawnPointCollection.transform.parent = this.transform;
        }
    }
    private void OnEnable()
    {
        gameManager.gameEvents.duskStarts += GenerateChests;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.duskStarts -= GenerateChests;
    }
    /// <summary>
    /// Spawn chests and assigns their value level
    /// </summary>
    private void GenerateChests()
    {
        if (chestSpawnPointCollection.transform.childCount <= 0)
        {
            Debug.LogWarning("No Chests Spawn Points were set! No Chests will spawn!");
            return;
        }
        // Clamp chest spawn count if too many chests will spawn
        if (chestSpawnCount > chestSpawnPointCollection.transform.childCount)
        {
            Debug.LogWarning("More chests were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {chestSpawnPointCollection.transform.childCount}!");
            chestSpawnCount = chestSpawnPointCollection.transform.childCount;
        }
        // Choose spawn locations
        Vector3[] spawnPoints = new Vector3[chestSpawnPointCollection.transform.childCount];
        for (int i = 0; i < chestSpawnPointCollection.transform.childCount; i++) spawnPoints[i] = chestSpawnPointCollection.transform.GetChild(i).transform.position;
        ShuffleArray(spawnPoints);

        // Spawn chests
        for (int i = 0; i < chestSpawnCount; i ++)
        {
            GameObject chest = new GameObject("Chest");
            chest.transform.position = spawnPoints[i];
        }
    }
    void ShuffleArray(Vector3[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, array.Length);

            Vector3 temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
