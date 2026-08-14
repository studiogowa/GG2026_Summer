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
    public float gameTimeRemaining { get { return gameEndTime - Time.time; } }

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
    private void Start()
    {
        StartDuskRound();
    }

    private void StartDuskRound()
    {
        gameState = GameState.Dusk;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + duskRoundTime;

        gameEvents.duskStarts?.Invoke();
        Debug.Log("Dusk Round Starts!");
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }

    private void StartDawnRound()
    {
        gameState = GameState.Dawn;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + dawnRoundTime;

        gameEvents.dawnStarts?.Invoke();
        Debug.Log("Dawn Round Starts!");
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }

    private void StartDayRound()
    {
        gameState = GameState.Day;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + dayRoundTime;

        gameEvents.duskStarts?.Invoke();
        Debug.Log("Day Round Starts!");
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }
    private Coroutine trackRoundProgressCoroutine;
    private IEnumerator TrackRoundProgress()
    {
        // Track round time progress
        while (Time.time < gameEndTime) // Time.time is affected by Time.timescale!
        {
            yield return null;
        }
        // Round Ends
        EndRound();
    }
    private void EndRound()
    {
        switch (gameState)
        {
            case GameState.Dusk:
                StartDawnRound();
                break;
            case GameState.Dawn:
                StartDayRound();
                break;
            case GameState.Day:
                StartDuskRound();
                break;
        }
    }
}

public enum GameState { Dusk, Dawn, Day }