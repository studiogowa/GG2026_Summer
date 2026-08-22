using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : Inventory
{
    public static ChestInventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ChestInventory found!");
            return;
        }

        instance = this;
    }
}
