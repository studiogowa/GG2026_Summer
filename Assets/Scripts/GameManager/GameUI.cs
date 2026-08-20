using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gamestateText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseScreen;

    private bool trackTime = false;
    private void OnEnable()
    {
        SubscribeFunctions();
    }
    private void OnDisable()
    {
        UnsubscribeFunctions();
    }
    private void Start()
    {
        SubscribeFunctions();

        hud.SetActive(true);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        UpdateRoundTime();
        if (Keyboard.current.escapeKey.wasPressedThisFrame) TogglePause();
    }

    private void SubscribeFunctions()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.gameEvents.duskStarts += UpdateGameState;
        GameManager.instance.gameEvents.dawnStarts += UpdateGameState;
        GameManager.instance.gameEvents.dayStarts += UpdateGameState;
        GameManager.instance.gameEvents.dayEnds += UpdateGameState;

        GameManager.instance.gameEvents.duskStarts += StartTrackingTime;
        GameManager.instance.gameEvents.dayEnds += StopTrackingTime;
    }
    private void UnsubscribeFunctions()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.gameEvents.duskStarts -= UpdateGameState;
        GameManager.instance.gameEvents.dawnStarts -= UpdateGameState;
        GameManager.instance.gameEvents.dayStarts -= UpdateGameState;
        GameManager.instance.gameEvents.dayEnds += UpdateGameState;

        GameManager.instance.gameEvents.duskStarts -= StartTrackingTime;
        GameManager.instance.gameEvents.dayEnds -= StopTrackingTime;
    }
    private void UpdateGameState()
    {
        if (GameManager.instance == null) return;
        gamestateText.text = $"{GameManager.instance.gameState}";
    }
    private void StartTrackingTime()
    {
        trackTime = true;
    }
    private void StopTrackingTime()
    {
        trackTime = false;
        timerText.text = "ROUND TIME";
    }
    private void UpdateRoundTime()
    {
        if (!trackTime || GameManager.instance == null) return;
        timerText.text = $"{Mathf.FloorToInt(GameManager.instance.gameTimeRemaining)}";
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
        hud.SetActive(false);
        pauseScreen.SetActive(true);  
    }
    private void UnpauseGame()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1.0f;
        hud.SetActive(true);
        pauseScreen.SetActive(false);
    }
}
