using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Upgrade", menuName = "Inventory/Inventory Upgrade")]

public class InventoryUpgrade : Item
{
    public int slotUpgrade;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    } 


    public override bool Use()
    {
        if (player.TryGetComponent<PlayerInventory>(out PlayerInventory inventory))
        {
            if (inventory.space < 20)
            {
                inventory.space = Mathf.Min(inventory.space + slotUpgrade, 20);
                inventory.onItemChangedCallback?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
