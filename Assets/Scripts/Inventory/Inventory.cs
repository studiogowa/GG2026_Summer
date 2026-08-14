using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item item, int amount)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            Item copyItem = Instantiate(item);
            copyItem.amount = amount;
            
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name && item.isStackable)
                {
                    items[i].amount += amount;
                    onItemChangedCallback.Invoke();
                    return true;
                }
            }

            items.Add(copyItem);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void Clear()
    {
        items.Clear();
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
