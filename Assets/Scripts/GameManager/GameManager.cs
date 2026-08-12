using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Progression Variables")]
    [field: SerializeField, Range(0.0f, 360.0f)] public float duskRoundTime { get; private set; } = 120.0f;
    [field: SerializeField, Range(0.0f, 360.0f)] public float dawnRoundTime { get; private set; } = 120.0f;
    [field: SerializeField, Range(0.0f, 360.0f)] public float dayRoundTime { get; private set; } = 120.0f;

    [field: SerializeField] public GameState gameState = GameState.Dusk;

    public float gameStartTime { get; private set; } = 0.0f;
    public float gameEndTime { get; private set; } = 0.0f;
    public float gameRoundTime { get { return Time.time - gameStartTime; } }
    public float gameTimeRemaining { get { return gameEndTime - gameStartTime; } }

    public GameEventsStruct gameEvents;
    public static GameManager instance;
    private void Awake()
    {   // Establish static reference
        if (GameManager.instance != null && GameManager.instance != this)
        {
            Debug.LogError("Another GameManager tried to Instantiate! Deleting!");
            Destroy(this.gameObject);
        }
        else GameManager.instance = this;
    }
    private void OnDestroy()
    {   // Remove static reference
        if (GameManager.instance != null && GameManager.instance == this) GameManager.instance = null;
    }

    private void StartDuskRound()
    {
        gameState = GameState.Dusk;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + duskRoundTime;

        gameEvents.duskStarts?.Invoke();
    }

    private void StartDawnRound()
    {
        gameState = GameState.Dawn;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + dawnRoundTime;

        gameEvents.dawnStarts?.Invoke();
    }

    private void StartDayRound()
    {
        gameState = GameState.Day;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + dayRoundTime;

        gameEvents.duskStarts?.Invoke();
    }

    private void EndRound()
    {
        switch (gameState)
        {
            case GameState.Dusk:
                break;
            case GameState.Dawn:
                break;
            case GameState.Day:
                break;
        }
    }
}

public enum GameState { Dusk, Dawn, Day }