using FMOD.Studio;
using FMODUnity;
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
    public EventReference useSFX;
    public int soundtype;
    private EventInstance useSFXInstance;

    public virtual bool Use()
    {
        // Use the item
        useSFXInstance = RuntimeManager.CreateInstance(useSFX);
        useSFXInstance.setParameterByName("ItemType", soundtype);
        useSFXInstance.start();
        useSFXInstance.release();
        Debug.Log("Using " + name);
        return true;
    }
}
