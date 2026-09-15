using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Loot : Interactable
{
    private LootInventory lootInventory;

    private InputAction moveAction;

    void Awake()
    {
        lootInventory = GetComponent<LootInventory>();

        moveAction = InputSystem.actions.FindAction("Move");
    }
    private void Update()
    {
        if (PlayerLeavesInteractionRadius()) CloseLoot();
    }
    public override void Interact()
    {
        if (LootUI.instance != null && LootUI.instance.inventory == lootInventory)
        {
            CloseLoot();
        } 
        else
        {
            OpenLoot();
        }
    }

    private void OpenLoot()
    {
        hasInteracted = true;
        LootUI.instance.OpenLootUI(lootInventory);

        moveAction.Disable();
    }

    private void CloseLoot()
    {
        hasInteracted = false;
        LootUI.instance.CloseLootUI();

        moveAction.Enable();        
    }
}
