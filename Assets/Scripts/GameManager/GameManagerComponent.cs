using UnityEngine;

public abstract class GameManagerComponent : MonoBehaviour
{
    protected GameManager gameManager;
    protected void Awake()
    {
        if (!TryGetComponent<GameManager>(out gameManager))
        {
            Debug.LogError($"{this.name} is a Game Manager Component that doesn't have a Game Manager Script! Disabling");
            this.enabled = false;
        }
    }
}