using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Chest : Interactable
{
    private ChestInventory chestInventory;

    private InputAction moveAction;

    void Awake()
    {
        chestInventory = GetComponent<ChestInventory>();

        moveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        if (PlayerLeavesInteractionRadius()) CloseChest();
    }
    public override void Interact()
    {
        if (ChestUI.instance != null && ChestUI.instance.inventory == chestInventory)
        {
            CloseChest();
        } 
        else
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        hasInteracted = true;
        ChestUI.instance.OpenChestUI(chestInventory);

        moveAction.Disable();
    }

    private void CloseChest()
    {
        hasInteracted = false;
        ChestUI.instance.CloseChestUI();

        moveAction.Enable();        
    }
}
