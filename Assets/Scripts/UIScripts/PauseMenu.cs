using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : GameUIComponent
{
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent<Animator>(out animator)) Debug.LogError($"{this.name} DOES NOT have an animator component!");
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) TogglePause();
    }

    private bool isPaused = false;
    private void TogglePause()
    {
        // Don't pause if it's a specific round
        if (GameManager.instance.gameState == GameState.PreGame || GameManager.instance.gameState == GameState.DayEnd) return;
        if (!isPaused) PauseGame();
        else UnpauseGame();
    }
    private void PauseGame()
    {
        if (isPaused) return;

        // 
        //  PUT PAUSING SOUND EFFECT HERE
        //

        isPaused = true;
        Time.timeScale = 0.0f;
        ui.hud.SetChildrenActive(false);
        animator.SetTrigger("Open");
    }
    private void UnpauseGame()
    {
        if (!isPaused) return;

        // 
        //  PUT UNPAUSING SOUND EFFECT HERE
        //

        isPaused = false;
        Time.timeScale = 1.0f;
        ui.hud.SetChildrenActive(true);
        animator.SetTrigger("Close");
    }
}
