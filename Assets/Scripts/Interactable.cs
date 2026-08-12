using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // how close a player needs to get to interact with an object
    public Transform interactionTransform;

    bool isFocus = false; // whether or not the object is currently being focused
    Transform player; // reference to the player transform
    bool hasInteracted = false; // whether or not the object has already been interacted with

    public virtual void Interact() // meant to be overridden
    {
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected () { // visualizing the radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
