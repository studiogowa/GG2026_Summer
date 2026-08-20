using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameUI : MonoBehaviour
{
    public HUD hud { get; private set; }
    public PauseMenu pauseMenu { get; private set; }

    private void Awake()
    {
        hud = GetComponentInChildren<HUD>();
        if (hud == null) Debug.LogError("GameManagerUI DOES NOT have a HUD component!");
        pauseMenu = GetComponentInChildren<PauseMenu>();
        if (pauseMenu == null) Debug.LogError("GameManagerUI DOES NOT have a PauseMenu component!");
    }
}