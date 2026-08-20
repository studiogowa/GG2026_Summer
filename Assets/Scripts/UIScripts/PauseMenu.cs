using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : GameUIComponent
{
    [SerializeField] private GameObject pauseMenu;
    private void Start()
    {
        pauseMenu.SetActive(false);
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
        ui.hud.hud.SetActive(false);
        pauseMenu.SetActive(true);
    }
    private void UnpauseGame()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1.0f;
        ui.hud.hud.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
