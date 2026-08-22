using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 20;
    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public virtual bool Add(Item item, int amount)
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
                if (items[i] != null && items[i].name == item.name && item.isStackable)
                {
                    items[i].amount += amount;
                    
                    if (onItemChangedCallback != null)
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

    public virtual void Remove(Item item)
    {
        items.Remove(item);
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public virtual void Clear()
    {
        items.Clear();
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
