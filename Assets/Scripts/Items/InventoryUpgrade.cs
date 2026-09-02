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


    public override void Use()
    {
        if (player.TryGetComponent<PlayerInventory>(out PlayerInventory inventory))
        {
            inventory.space += slotUpgrade;
            inventory.onItemChangedCallback?.Invoke();
        }
    }
}
