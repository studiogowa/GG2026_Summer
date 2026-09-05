using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using FMODUnity;
public class PerformanceReview : GameUIComponent
{
    private Animator animator;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI chestsFilledText;
    [SerializeField] private TextMeshProUGUI chestQualityRatingText;
    [SerializeField] private TextMeshProUGUI thirdThingText;
    [SerializeField] private TextMeshProUGUI finalRatingText;

    [SerializeField] private Button continueButton;

    [Header("Audio")]
    [SerializeField] private EventReference ShowPerfReviewSFX;
    [SerializeField] private EventReference ChestCountSFX;
    [SerializeField] private EventReference ChestQualitySFX;
    [SerializeField] private EventReference TypingSFX;
    [SerializeField] private EventReference StampSFX;

    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent<Animator>(out animator)) Debug.LogError($"{this.name} DOES NOT have an animator component!");
    }
    private void OnDisable()
    {
        UnsubscribeFunctions();
    }
    private void SubscribeFunctions()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.gameEvents.preGameStarts += CloseMenu;
        GameManager.instance.gameEvents.performanceReviewStarts += OpenMenu;
        continueButton.onClick.AddListener(GameManager.instance.StartGame);
    }
    private void UnsubscribeFunctions()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.gameEvents.preGameStarts -= CloseMenu;
        GameManager.instance.gameEvents.performanceReviewStarts -= OpenMenu;
        continueButton.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        SubscribeFunctions();
    }
    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame) ToggleMenu();
    }
    private bool menuOpened = false;
    private void ToggleMenu()
    {
        RuntimeManager.PlayOneShot(ShowPerfReviewSFX);
        if (!menuOpened) OpenMenu();
        else CloseMenu();
    }
    private void OpenMenu()
    {
        if (menuOpened) return;
        menuOpened = true;
        SubscribeFunctions();
        animator.SetTrigger("Open");

        if (calcualtePerformanceCoroutine != null) StopCoroutine(calcualtePerformanceCoroutine);
        calcualtePerformanceCoroutine = StartCoroutine(CalculatePerformance());
    }
    private void CloseMenu()
    {
        if (!menuOpened) return;
        menuOpened = false;
        UnsubscribeFunctions();
        animator.SetTrigger("Close");
    }

    [Header("Timing Variables for Performance Review")]
    [SerializeField, Range(0.0f, 2.0f)] private float initialPause = 0.5f;
    [SerializeField, Range(0.0f, 2.0f)] private float titleDisplayPause = 0.4f;
    [SerializeField, Range(0.0f, 2.0f)] private float valueDisplayPause = 0.2f;
    [SerializeField, Range(0.0f, 2.0f)] private float chestCountTime = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float chestQualityTime = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float finalRatingTime = 1.5f;
    [SerializeField, Range(0.0f, 2.0f)] private float finalRatingPause = 1.5f;
    [SerializeField, Range(0.0f, 2.0f)] private float continueButtonRevealDelay = 2.0f;
    private Coroutine calcualtePerformanceCoroutine;

    [Header("Chest Quality Strategy")]
    [Tooltip("0 for item amount quality, 1 for value quality")]
    [SerializeField, Range(0, 1)] private int strategy = 0;
    private IEnumerator CalculatePerformance()
    {
        ComponentsSetAllActive(false);
        yield return new WaitForSeconds(initialPause);
        RuntimeManager.PlayOneShot(TypingSFX);
        titleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(titleDisplayPause);

        // Count Chests Filled
        float timeStart = Time.time;
        chestsFilledText.gameObject.SetActive(true);
        RuntimeManager.PlayOneShot(ChestCountSFX);
        while (Time.time < timeStart + chestCountTime)
        {
            chestsFilledText.text = $"Chests Filled: {Random.Range(0, GameManager.instance.chestSpawner.chestsSpawned)}/ {GameManager.instance.chestSpawner.chestsSpawned}";
            yield return null;
        }
        chestsFilledText.text = $"Chests Filled: {GetChestsFilledCount()}/ {GameManager.instance.chestSpawner.chestsSpawned}";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Chest Quality
        timeStart = Time.time;
        chestQualityRatingText.gameObject.SetActive(true);
        RuntimeManager.PlayOneShot(ChestQualitySFX);
        while (Time.time < timeStart + chestQualityTime)
        {
            chestQualityRatingText.text = $"Chest Quality: {Random.Range(0.0f, 100.0f):0}/ 100%";
            yield return null;
        }
        int shiftScore = Mathf.RoundToInt(CalculateChestQuality(strategy));
        chestQualityRatingText.text = $"Chest Quality: {shiftScore}/ 100%";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Overall Rating
        finalRatingText.gameObject.SetActive(true);
        RuntimeManager.PlayOneShot(TypingSFX);
        string resultsText = "RESULTS";
        finalRatingText.text = "";
        for (int i = 0; i < resultsText.Length; i ++)
        {
            if (i != 0) yield return new WaitForSeconds(finalRatingTime / (resultsText.Length - 1));
            finalRatingText.text += resultsText[i];
            RuntimeManager.PlayOneShot(TypingSFX);
        }
        yield return new WaitForSeconds(finalRatingPause);

        // Display Shift Rating
        if (GameManager.instance.DeterminePassOrFail(shiftScore)) finalRatingText.text = "Okay I guess";
        else finalRatingText.text = "Disappointing";

        RuntimeManager.PlayOneShot(StampSFX);
        yield return new WaitForSeconds(continueButtonRevealDelay);
        continueButton.gameObject.SetActive(true);
    }
    private void ComponentsSetAllActive(bool isActive)
    {
        titleText.gameObject.SetActive(isActive);
        chestsFilledText.gameObject.SetActive(isActive);
        chestQualityRatingText.gameObject.SetActive(isActive);
        thirdThingText.gameObject.SetActive(isActive);
        finalRatingText.gameObject.SetActive(isActive);

        continueButton.gameObject.SetActive(isActive);
    }
    /// <summary>
    /// Tallies the number of Chests that are filled by the Player
    /// </summary>
    /// <returns>The number of non-empty Chests</returns>
    public int GetChestsFilledCount()
    {
        int count = 0;
        foreach (Transform currTransform in GameManager.instance.chestSpawner.chestCollection.transform)
        {
            if (currTransform.TryGetComponent<ChestInventory>(out ChestInventory currChestInventory) && !currChestInventory.IsEmpty()) count++;
        }

        return count;
    }
    /// <summary>
    /// Calculates the mean average of the chest quality of all chests
    /// </summary>
    /// <returns>A percentage representing overall chest quality</returns>
    public float CalculateChestQuality(int strategy)
    {
        float qualitySum = 0;
        foreach (Transform currTransform in GameManager.instance.chestSpawner.chestCollection.transform)
        {
            if (currTransform.TryGetComponent<ChestInventory>(out ChestInventory currChestInventory))
            {
                qualitySum += currChestInventory.CalculateChestQuality(strategy);
            }
        }

        return qualitySum/ GameManager.instance.chestSpawner.chestSpawnCount;
    }
}
