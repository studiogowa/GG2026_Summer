using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    [SerializeField] private Text gamestateText;
    [SerializeField] private Text timerText;

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
    }
    private void UnsubscribeFunctions()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.gameEvents.duskStarts -= UpdateGameState;
        GameManager.instance.gameEvents.dawnStarts -= UpdateGameState;
        GameManager.instance.gameEvents.dayStarts -= UpdateGameState;
    }
    private void UpdateGameState()
    {
        if (GameManager.instance == null) return;
        gamestateText.text = GameManager.instance.gameState.ToString();
    }
    private void UpdateRoundTime()
    {
        if (GameManager.instance == null) return;
        timerText.text = GameManager.instance.gameTimeRemaining.ToString();
    }
}
