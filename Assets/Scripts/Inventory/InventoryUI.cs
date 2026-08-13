using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    private InputAction inventoryToggleAction;
    Inventory inventory;
    InventorySlot[] slots;

    private void OnEnable()
    {
        inventoryToggleAction = InputSystem.actions.FindAction("Inventory");

        if (inventoryToggleAction != null)
        {
            inventoryToggleAction.performed += OnInventoryToggle;
            inventoryToggleAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (inventoryToggleAction != null)
        {
            inventoryToggleAction.performed -= OnInventoryToggle;
            inventoryToggleAction.Disable();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void OnInventoryToggle(InputAction.CallbackContext context)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
