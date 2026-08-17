using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TextMeshProUGUI amount;
    protected Item item;

    public void Start()
    {
        if (amount != null)
            amount.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        if (amount != null)
        {
            amount.enabled = true;
            amount.text = item.amount.ToString("n0");
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;

        if (amount != null)
            amount.enabled = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        Debug.Log("UseItem called");
        if (item != null)
        {
            Debug.Log("Item is: " + item.name);
            item.Use();
        }
        else
        {
            Debug.Log("Item is null!");
        }
    }
}
