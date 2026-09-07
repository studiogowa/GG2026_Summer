using UnityEngine;

public class Door : Interactable
{
    private Collider2D doorCollider;
    private Animator doorAnimator;
    [SerializeField] private Item doorKey;
    [SerializeField] private bool isOpened = false;
    private void Awake()
    {
        if (!TryGetComponent<Collider2D>(out doorCollider)) Debug.LogError("This Door DOES NOT have a Collider2D Component!");
        if (!TryGetComponent<Animator>(out doorAnimator)) Debug.LogError("This Door DOES NOT have an Animator Component!");
    }

    public override void Interact()
    {
        if (isOpened || PlayerInventory.instance == null) return;

        Item playersKey = PlayerInventory.instance.items.Find(x => x.name == doorKey.name);
        if (playersKey != null)
        {
            PlayerInventory.instance.Remove(playersKey, 1);
            Unlock();
        }
        else Debug.Log("No Key available!");
    }

    private void Unlock()
    {
        isOpened = true;
        doorAnimator.SetTrigger("Open");
        doorCollider.enabled = false;
        Debug.Log("Unlocked Door!");
    }
    public void Lock()
    {
        isOpened = false;
        doorAnimator.SetTrigger("Close");
        doorCollider.enabled = true;
    }
}
