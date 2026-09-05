using UnityEngine;
using System.Linq;
public class ResourceGenerator : GameManagerComponent
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private ItemPool bodyItemPool;
    public GameObject resourceCollection { get; private set; }
    private int resourceSpawnCount = 6;
    protected override void Awake()
    {
        base.Awake();
        if (bodyPrefab == null) Debug.LogError("Reference the Body Prefab for Resource Generator!");
        if (bodyItemPool == null) Debug.LogError("Include an Item pool for Lootable Bodies for Resource Generator!");
        if (resourceCollection == null)
        {
            resourceCollection = new GameObject("RESOURCE COLLECTION");
            resourceCollection.transform.parent = this.transform;
        }
    }
    private void OnEnable()
    {
        gameManager.gameEvents.preGameStarts += SetupResources;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.preGameStarts -= SetupResources;
    }
    private void SetupResources()
    {
        ClearResources();
        GenerateResources();
    }
    private void ClearResources()
    {
        for (int i = resourceCollection.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(resourceCollection.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// Generates dead bodies with a random amount of lootable items on them
    /// </summary>
    private void GenerateResources()
    {
        if (gameManager.dungeonManager.resourceSpawnPointCount <= 0)
        {
            Debug.LogWarning("No Resource Spawn Points were set! No Resources will spawn!");
            return;
        }
        resourceSpawnCount = gameManager.currShiftData.lootablesCount;
        // Clamp resource spawn count if too many resources will spawn
        if (resourceSpawnCount > gameManager.dungeonManager.resourceSpawnPointCount)
        {
            Debug.LogWarning("More Resources were going to be spawned than there are spawn points! " +
                $"Limiting spawn count to {gameManager.dungeonManager.resourceSpawnPointCount}!");
            resourceSpawnCount = gameManager.dungeonManager.resourceSpawnPointCount;
        }
        // Choose spawn locations
        Vector3[] spawnPoints = gameManager.dungeonManager.GetResourceSpawns(resourceSpawnCount);

        // Spawn Resources
        int currResourceCount = 0;
        foreach (Vector3 spawnCoords in spawnPoints)
        {
            GameObject body = Instantiate(bodyPrefab);
            body.transform.parent = resourceCollection.transform;
            body.transform.position = spawnCoords;

            if (!body.TryGetComponent<LootInventory>(out LootInventory inventory)) Debug.LogWarning($"Spawned {body.name} DOES NOT have a LootInventory Component!");
            else
            {
                ItemPoolItem[] itemArray = ChooseItem(bodyItemPool, gameManager.currShiftData.lootables[currResourceCount].itemCount);
                foreach (ItemPoolItem item in itemArray) inventory.Add(item.item, Random.Range(item.stackableMinAmount, item.stackableMaxAmount+1));
            }

            currResourceCount++;
        }
    }
    /// <summary>
    /// Chooses itemCount different items from itemPool to spawn in a Body<br></br>
    /// Duplicates become allowed when itemCount exceeds the number of items in itemPool!
    /// </summary>
    /// <param name="itemPool">The item pool to pick from</param>
    /// <param name="itemCount">The number of items to pick from a spawn pool</param>
    /// <returns>Returns an Array of [itemCount] ItemPoolItem elements</returns>
    private ItemPoolItem[] ChooseItem(ItemPool itemPool, int itemCount)
    {
        if (itemPool.itemPoolItems.Count <= 0)
        {
            Debug.LogError("Given item pool HAS NO ITEMS!");
            return new ItemPoolItem[0];
        }

        ItemPoolItem[] itemArray = new ItemPoolItem[itemCount];
        int[] indices = new int[0];

        // Create an array [0, 1 ... itemPoolItems.Count - 1]
        // Continue to grow this array by concatenating it to itself itemCount is GREATER than the itemPool item count
        while (indices.Length < itemCount)
        {
            int[] tempIndices = new int[itemPool.itemPoolItems.Count];
            for (int i = 0; i < itemPool.itemPoolItems.Count; i++) tempIndices[i] = i;
            indices = indices.Concat(tempIndices).ToArray();
        }
        
        // Shuffle the indices, then use the first itemCount numbers as a selection of random indices for itemPool items
        ArrayHelpers.ShuffleArray<int>(indices);
        for (int i = 0; i < itemCount; i++) itemArray[i] = itemPool.itemPoolItems[indices[i]];

        return itemArray;
    }
}