using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public Inventory inventory;

    protected InputAction inventoryToggleAction;
    protected InventorySlot[] slots;

    protected virtual void OnEnable()
    {
        inventoryToggleAction = InputSystem.actions.FindAction("Inventory");

        if (inventoryToggleAction != null)
        {
            inventoryToggleAction.performed += OnInventoryToggle;
            inventoryToggleAction.Enable();
        }
    }

    protected virtual void OnDisable()
    {
        if (inventoryToggleAction != null)
        {
            inventoryToggleAction.performed -= OnInventoryToggle;
            inventoryToggleAction.Disable();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (itemsParent != null)
        {
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        }
        else
        {
            slots = null;
        }

        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }

        if (inventory == null && GetType() == typeof(InventoryUI))
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
                inventory = player.GetComponent<Inventory>();
        } 

        if (inventory != null) 
        {
            inventory.onItemChangedCallback += UpdateUI;
            UpdateUI();
        }
    }

    protected virtual void OnInventoryToggle(InputAction.CallbackContext context)
    {
        if (inventoryUI == null)
            return;

        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    protected virtual void UpdateUI()
    {
        if (inventory == null || slots == null)
        {
            ClearAllSlots();
            return;
        }
    
        Debug.Log($"[InventoryUI Debug] Slots array size: {slots.Length} | Allowed Space count: {inventory.space}");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.space)
            {
                slots[i].gameObject.SetActive(true);

                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i], inventory);

                    if (slots[i].amount != null)
                    {
                        if (inventory.items[i].amount > 1)
                        {
                            slots[i].amount.enabled = true;
                            slots[i].amount.text = inventory.items[i].amount.ToString("n0");
                        }
                        else
                        {
                            slots[i].amount.enabled = false;
                        }
                    }
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    protected void ClearAllSlots()
    {
        if (slots == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].ClearSlot();
            }
        }
    }
}
