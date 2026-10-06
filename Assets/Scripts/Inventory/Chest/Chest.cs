using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Chest : Interactable
{
    private SpriteRenderer chestRenderer;
    [SerializeField] private Sprite chestClosed;
    [SerializeField] private Sprite chestOpened;

    private ChestInventory chestInventory;

    private InputAction moveAction;

    void Awake()
    {
        chestRenderer = GetComponent<SpriteRenderer>();
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
        chestRenderer.sprite = chestOpened;

        hasInteracted = true;
        ChestUI.instance.OpenChestUI(chestInventory);

        moveAction.Disable();
    }
    private void CloseChest()
    {
        chestRenderer.sprite = chestClosed;

        hasInteracted = false;
        ChestUI.instance.CloseChestUI();

        moveAction.Enable();        
    }
}
