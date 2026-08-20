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

    public override void Interact()
    {
        //base.Interact();

        if (hasInteracted)
        {
            Debug.Log("Closing chest");
            hasInteracted = false;
            ChestUI.instance.CloseChestUI();

            moveAction.Enable();
        } else
        {
            Debug.Log("Opening chest");
            hasInteracted = true;
            ChestUI.instance.OpenChestUI(chestInventory);

            moveAction.Disable();
        }
    }
}
