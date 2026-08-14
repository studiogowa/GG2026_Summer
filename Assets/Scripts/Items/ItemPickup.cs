using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public int amount = 1;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item, amount);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
