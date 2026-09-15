using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // how close a player needs to get to interact with an object
    public Transform interactionTransform; // where a player needs to be to interact with an object

    bool isFocus = false;
    Transform player;
    public bool hasInteracted = false;

    public virtual void Interact() // meant to be overridden
    {
        Debug.Log($"Interacting with " + transform.name);
    }

/*
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance > radius)
            {
                isFocus = false;
                hasInteracted = false;
            }
        }
    }
    */

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        //hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        //hasInteracted = false;
    }
    /// <summary>
    /// Check whether Player has left the interaction radius
    /// </summary>
    /// <returns>True if they have left the radius, False otherwise</returns>
    protected bool PlayerLeavesInteractionRadius()
    {
        if (player == null) return false;
        if (hasInteracted && Vector3.Distance(transform.position, player.position) >= radius) return true;
        else return false;
    }

    void OnDrawGizmosSelected () // visualizing the radius in the editor
    { 
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
