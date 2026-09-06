using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using FMODUnity;

public class Chest : Interactable
{
    private ChestInventory chestInventory;

    private InputAction moveAction;

    void Awake()
    {
        chestInventory = GetComponent<ChestInventory>();

        moveAction = InputSystem.actions.FindAction("Move");
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
        RuntimeManager.PlayOneShot(openSFX);
        hasInteracted = true;
        ChestUI.instance.OpenChestUI(chestInventory);

        moveAction.Disable();
    }

    private void CloseChest()
    {
        RuntimeManager.PlayOneShot(closeSFX);
        hasInteracted = false;
        ChestUI.instance.CloseChestUI();

        moveAction.Enable();        
    }
}
