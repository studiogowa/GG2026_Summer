using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    public LayerMask interactionMask;
    public Interactable focus;

    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;

    [SerializeField, Range(0.0f, 10.0f)] private float movementSpeed = 5.0f;

    private void Awake()
    {
        if (!this.TryGetComponent<Collider2D>(out playerCollider)) Debug.LogError("Player DOES NOT have a collider!");
        if (!this.TryGetComponent<Rigidbody2D>(out playerRigidbody)) Debug.LogError("Player DOES NOT have a rigidbody!");

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
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        // Left mouse button to remove focus
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RemoveFocus();
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
        focus.OnDefocused();
        focus = null;
    }

    private void MoveCharacter()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        playerRigidbody.linearVelocity = movementInput.normalized * movementSpeed;
    }
}
