using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public int amount = 1;
    private void Start()
    {
        if (GameManager.instance != null) this.transform.parent = GameManager.instance.droppedItems.container.transform;
    }
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = PlayerInventory.instance.Add(item, amount);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
