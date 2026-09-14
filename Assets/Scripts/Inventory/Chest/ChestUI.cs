using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ChestUI : InventoryUI
{
    public static ChestUI instance;
    public InventoryUI playerUI;
    [SerializeField] private TextMeshProUGUI valueCounter;

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

        if (playerUI != null)
        {
            playerUI.inventoryUI.SetActive(true);
        }
    }

    public void CloseChestUI()
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

    protected override void UpdateUI()
    {
        base.UpdateUI();

        // Update Value Target Counter
        string valueTarget = "??";
        if (inventory is ChestInventory currChestInventory) valueTarget = $"{currChestInventory.valueTarget}";
        valueCounter.text = $"{inventory.GetTotalValue()} / {valueTarget}";
    }
}
