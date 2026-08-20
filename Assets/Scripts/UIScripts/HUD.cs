using UnityEngine;
using TMPro;
public class HUD : GameUIComponent
{
    [SerializeField] private TextMeshProUGUI gamestateText;
    [SerializeField] private TextMeshProUGUI timerText;

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
        gameObject.SetActive(true);
        SubscribeFunctions();
    }
    private void Update()
    {
        UpdateRoundTime();
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
}
