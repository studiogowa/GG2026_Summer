using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "";
    public Sprite icon = null;
    public int amount;
    public int value;
    public bool isStackable = false;
    public bool isDefaultItem = false;
    public GameObject prefab;

    public virtual void Use()
    {
        // Use the item
        
        Debug.Log("Using " + name);
    }
}
