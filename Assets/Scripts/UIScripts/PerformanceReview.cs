using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class PerformanceReview : GameUIComponent
{
    private Animator animator;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI chestsFilledText;
    [SerializeField] private TextMeshProUGUI chestQualityRatingText;
    [SerializeField] private TextMeshProUGUI thirdThingText;
    [SerializeField] private TextMeshProUGUI finalRatingText;

    [SerializeField] private Button continueButton;
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
    private IEnumerator CalculatePerformance()
    {
        ComponentsSetAllActive(false);
        yield return new WaitForSeconds(initialPause);
        // Display Title
        titleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(titleDisplayPause);

        // Count Chests Filled
        float timeStart = Time.time;
        chestsFilledText.gameObject.SetActive(true);
        while (Time.time < timeStart + chestCountTime)
        {
            chestsFilledText.text = $"Chests Filled: {Random.Range(0, GameManager.instance.chestSpawner.chestsSpawned)}/ {GameManager.instance.chestSpawner.chestsSpawned}";
            yield return null;
        }
        chestsFilledText.text = $"Chests Filled: ?/ {GetChestsFilledCount()}";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Chest Quality
        timeStart = Time.time;
        chestQualityRatingText.gameObject.SetActive(true);
        while (Time.time < timeStart + chestQualityTime)
        {
            chestQualityRatingText.text = $"Chest Quality: {Random.Range(50.0f, 100.0f):0}/ 100%";
            yield return null;
        }
        chestQualityRatingText.text = $"Chest Quality: {CalculateChestQuality():0}/ 100%";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Overall Rating
        finalRatingText.gameObject.SetActive(true);
        string resultsText = "RESULTS";
        finalRatingText.text = "";
        for (int i = 0; i < resultsText.Length; i ++)
        {
            if (i != 0) yield return new WaitForSeconds(finalRatingTime / (resultsText.Length - 1));
            finalRatingText.text += resultsText[i];  
        }

        yield return new WaitForSeconds(finalRatingPause);
        finalRatingText.text = "Okay I guess";

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

    public int GetChestsFilledCount()
    {
        int count = 0;
        foreach (Transform currTransform in GameManager.instance.chestSpawner.chestCollection.transform)
        {
            if (currTransform) count++;
        }

        return count;
    }
    public float CalculateChestQuality()
    {
        float quality = 0;
        foreach (Transform currTransform in GameManager.instance.chestSpawner.chestCollection.transform)
        {
            if (currTransform) quality = Random.Range(50.0f, 100.0f);
        }

        return quality;
    }
}
