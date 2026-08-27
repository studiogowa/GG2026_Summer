using UnityEngine;
using UnityEngine.InputSystem;

public class LootUI : InventoryUI
{
    public static LootUI instance;
    public InventoryUI playerUI;

    private void Awake()
    {
        instance = this;
    }

    protected override void OnEnable() {}
    protected override void OnDisable()
    {
        if (inventory != null)
            inventory.onItemChangedCallback -= UpdateUI;
    }

    protected override void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryUI.SetActive(false);
    }

    public void OpenLootUI(Inventory lootInventory)
    {
        if (inventory != null)
        {
            inventory.onItemChangedCallback -= UpdateUI;
        }

        inventory = lootInventory;
        inventory.onItemChangedCallback += UpdateUI;

        inventoryUI.SetActive(true);
        UpdateUI();
        
        if (playerUI != null)
        {
            playerUI.inventoryUI.SetActive(true);
        }
    }

    public void CloseLootUI()
    {
        if (inventory != null)
        {
            inventory.onItemChangedCallback -= UpdateUI;
        }

        inventory = null;
        inventoryUI.SetActive(false);

        if (playerUI != null)
        {
            playerUI.inventoryUI.SetActive(false);
        }
    }
}
