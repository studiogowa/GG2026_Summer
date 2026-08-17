using UnityEngine;
using UnityEngine.InputSystem;

public class ChestUI : InventoryUI
{
    public static ChestUI instance;

    private void Awake()
    {
        instance = this;
    }

    protected override void OnEnable() {}
    protected override void OnDisable() {}

    protected override void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryUI.SetActive(false);
    }

    public void OpenChestUI(Inventory chestInventory)
    {
        if (inventory != null)
        {
            inventory.onItemChangedCallback -= UpdateUI;
        }

        inventory = chestInventory;
        inventory.onItemChangedCallback += UpdateUI;

        inventoryUI.SetActive(true);
        UpdateUI();
    }

    public void CloseChestUI()
    {
        if (inventory != null)
        {
            inventory.onItemChangedCallback -= UpdateUI;
        }

        inventory = null;
        inventoryUI.SetActive(false);
    }
}
