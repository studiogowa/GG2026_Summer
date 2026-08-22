using UnityEngine;
using UnityEngine.InputSystem;
public class PerformanceReview : GameUIComponent
{
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent<Animator>(out animator)) Debug.LogError($"{this.name} DOES NOT have an animator component!");
    }
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame) ToggleMenu();
    }
    private bool menuOpened = false;
    private void ToggleMenu()
    {
        if (!menuOpened) OpenMenu();
        else CloseMenu();
    }
    private void OpenMenu()
    {
        if (menuOpened) return;
        menuOpened = true;
        animator.SetTrigger("Open");
    }
    private void CloseMenu()
    {
        if (!menuOpened) return;
        menuOpened = false;
        animator.SetTrigger("Close");
    }
}
