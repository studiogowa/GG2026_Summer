using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using FMODUnity;

public class Loot : Interactable
{
    private LootInventory lootInventory;

    private InputAction moveAction;

    void Awake()
    {
        lootInventory = GetComponent<LootInventory>();

        moveAction = InputSystem.actions.FindAction("Move");
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
        RuntimeManager.PlayOneShot(openSFX);
        hasInteracted = true;
        LootUI.instance.OpenLootUI(lootInventory);

        moveAction.Disable();
    }

    private void CloseLoot()
    {
        RuntimeManager.PlayOneShot(closeSFX);
        hasInteracted = false;
        LootUI.instance.CloseLootUI();

        moveAction.Enable();        
    }
}
