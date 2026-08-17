using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private ChestInventory chestInventory;

    void Awake()
    {
        chestInventory = GetComponent<ChestInventory>();
    }

    public override void Interact()
    {
        base.Interact();

        ChestUI.instance.OpenChestUI(chestInventory);
    }
}
