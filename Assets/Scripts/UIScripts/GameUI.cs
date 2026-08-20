using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private HUD hud;

    private void Awake()
    {
        hud = GetComponentInChildren<HUD>();
        if (hud == null) Debug.LogError("GameManagerUI DOES NOT have a HUD component!");
    }
    private void Start()
    {
        hud.gameObject.SetActive(true);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) TogglePause();
    }

    private bool isPaused = false;
    private void TogglePause()
    {
        if (!isPaused) PauseGame();
        else UnpauseGame();
    }
    private void PauseGame()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0.0f;
        hud.gameObject.SetActive(false);
        pauseScreen.SetActive(true);  
    }
    private void UnpauseGame()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1.0f;
        hud.gameObject.SetActive(true);
        pauseScreen.SetActive(false);
    }
}
