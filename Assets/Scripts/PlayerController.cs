using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    private void Awake()
    {
        if (!this.TryGetComponent<Collider2D>(out playerCollider)) Debug.LogError("Player DOES NOT have a collider!");
    }

    void Update()
    {
        
    }
}
