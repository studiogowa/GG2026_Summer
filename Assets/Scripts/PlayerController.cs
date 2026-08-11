using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;

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
    }
    private void MoveCharacter()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        playerRigidbody.linearVelocity = movementInput.normalized * movementSpeed;
    }
}
