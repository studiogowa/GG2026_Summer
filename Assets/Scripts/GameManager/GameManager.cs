using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    [Header("Game Progression Variables")]
    [SerializeField] private List<ShiftData> shiftData;
    [SerializeField] private int currShift = 0;

    [field: SerializeField] public GameState gameState = GameState.Dusk;

    public float gameStartTime { get; private set; } = 0.0f;
    public float gameEndTime { get; private set; } = 0.0f;
    public float gameRoundTime { get { return Time.time - gameStartTime; } }
    public float gameTimeRemaining { get { return gameEndTime - Time.time; } }

    public GameEventsStruct gameEvents;
    public static GameManager instance;

    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public ResourceGenerator resourceGenerator;
    [HideInInspector] public ChestSpawner chestSpawner;
    [HideInInspector] public ExplorerSpawner explorerSpawner;
    [HideInInspector] public DungeonManager dungeonManager;
    private void Awake()
    {   // Establish static reference
        if (GameManager.instance != null && GameManager.instance != this)
        {
            Debug.LogError("Another GameManager tried to Instantiate! Deleting!");
            Destroy(this.gameObject);
        }
        else GameManager.instance = this;

        if (!TryGetComponent<PlayerManager>(out playerManager)) Debug.LogError("Game Manager is missing a Player Manager Component!");
        if (!TryGetComponent<ResourceGenerator>(out resourceGenerator)) Debug.LogError("Game Manager is missing a Resource Generator Component!");
        if (!TryGetComponent<ChestSpawner>(out chestSpawner)) Debug.LogError("Game Manager is missing a Chest Spawner Component!");
        if (!TryGetComponent<ExplorerSpawner>(out explorerSpawner)) Debug.LogError("Game Manager is missing a Explorer Spawner Component!");
        if (!TryGetComponent<DungeonManager>(out dungeonManager)) Debug.LogError("Game Manager is missing a Dungeon Manager Component!");
    }
    private void OnDestroy()
    {   // Remove static reference
        if (GameManager.instance != null && GameManager.instance == this) GameManager.instance = null;
    }

    public void StartGame()
    {
        StopAllCoroutines();
        StartPreGameSetup();
    }
    private void StartPreGameSetup()
    {
        gameState = GameState.PreGame;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + shiftData[currShift].duskRoundTime;

        gameEvents.preGameStarts?.Invoke();
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }
    private void StartDuskRound()
    {
        gameState = GameState.Dusk;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + shiftData[currShift].duskRoundTime;

        gameEvents.duskStarts?.Invoke();
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }
    private void StartDawnRound()
    {
        gameState = GameState.Dawn;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + shiftData[currShift].dawnRoundTime;

        gameEvents.dawnStarts?.Invoke();
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }
    private void StartDayRound()
    {
        gameState = GameState.Day;

        gameStartTime = Time.time;
        gameEndTime = gameStartTime + shiftData[currShift].dayRoundTime;

        gameEvents.dayStarts?.Invoke();
        trackRoundProgressCoroutine = StartCoroutine(TrackRoundProgress());
    }
    private void DayEnd()
    {
        gameState = GameState.DayEnd;

        if (dungeonManager.IsInExtractionArea(player.transform.position)) Debug.Log("PLAYER IS IN EXTRACTION");
        else Debug.Log("PLAYER IS NOT IN EXTRACTION");

        gameEvents.dayEnds?.Invoke();
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
        StartCoroutine(EndRound());
    }
    private IEnumerator EndRound()
    {
        switch (gameState)
        {
            case GameState.PreGame:
                StartDuskRound();
                break;
            case GameState.Dusk:
                gameEvents.duskEnds?.Invoke();
                yield return new WaitForSeconds(shiftData[currShift].preDawnTime);
                StartDawnRound();
                break;
            case GameState.Dawn:
                gameEvents.dawnEnds?.Invoke();
                yield return new WaitForSeconds(shiftData[currShift].preDayTime);
                StartDayRound();
                break;
            case GameState.Day:
                DayEnd();
                yield return new WaitForSeconds(shiftData[currShift].dayEndPause);
                gameEvents.performanceReviewStarts?.Invoke();
                break;
        }

        yield break;
    }
}

public enum GameState { PreGame, Dusk, Dawn, Day, DayEnd }