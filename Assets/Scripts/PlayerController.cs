using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    public LayerMask interactionMask;
    public Interactable focus;

    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        if (!this.TryGetComponent<Collider2D>(out playerCollider)) Debug.LogError("Player DOES NOT have a collider!");
        if (!this.TryGetComponent<Rigidbody2D>(out playerRigidbody)) Debug.LogError("Player DOES NOT have a rigidbody!");
        if (!this.TryGetComponent<PlayerStats>(out playerStats)) Debug.LogError("Player DOES NOT have player stats!");
        
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        MoveCharacter();

        // Right mouse button to set focus
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            
            Collider2D hit = Physics2D.OverlapPoint(worldPos, interactionMask);
            if (hit != null)
            {
                Interactable interactable = hit.GetComponent<Interactable>();
                if (interactable != null && Vector3.Distance(transform.position, interactable.interactionTransform.position) <= interactable.radius)
                {
                    interactable.Interact();

                    // Only focus if the chest successfully opened
                    if (interactable.hasInteracted)
                    {
                        SetFocus(interactable);
                    }
                    else
                    {
                        RemoveFocus();
                    }
                 }
            }   
        }    
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus == null)
            return;

        focus.OnDefocused();
        focus = null;
    }

    private void MoveCharacter()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        playerRigidbody.linearVelocity = movementInput.normalized * playerStats.movementSpeed;
    }

}