using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
public class PerformanceReview : GameUIComponent
{
    private Animator animator;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI chestsFilledText;
    [SerializeField] private TextMeshProUGUI chestQualityRatingText;
    [SerializeField] private TextMeshProUGUI thirdThingText;
    [SerializeField] private TextMeshProUGUI finalRatingText;
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
        StartCoroutine(CalculatePerformance());
    }
    private void CloseMenu()
    {
        if (!menuOpened) return;
        menuOpened = false;
        animator.SetTrigger("Close");
    }

    [SerializeField, Range(0.0f, 2.0f)] private float initialPause = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float valueDisplayPause = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float chestCountTime = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float chestQualityTime = 1.0f;
    [SerializeField, Range(0.0f, 2.0f)] private float finalRatingTime = 1.0f;
    private IEnumerator CalculatePerformance()
    {
        TextSetAllActive(false);
        yield return new WaitForSeconds(initialPause);
        // Display Title
        titleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(valueDisplayPause * 2.0f);

        // Count Chests Filled
        float timeStart = Time.time;
        chestsFilledText.gameObject.SetActive(true);
        while (Time.time < timeStart + chestCountTime)
        {
            chestsFilledText.text = $"Chests Filled: {Random.Range(0, GameManager.instance.chestSpawner.chestsSpawned)}/ {GameManager.instance.chestSpawner.chestsSpawned}";
            yield return null;
        }
        chestsFilledText.text = $"Chests Filled: ?/ {GameManager.instance.chestSpawner.chestsSpawned}";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Chest Quality
        timeStart = Time.time;
        chestQualityRatingText.gameObject.SetActive(true);
        while (Time.time < timeStart + chestQualityTime)
        {
            chestQualityRatingText.text = $"Chest Quality: {Random.Range(50.0f, 100.0f):0}/ 100%";
            yield return null;
        }
        chestQualityRatingText.text = $"Chest Quality: {Random.Range(50.0f, 100.0f):0}/ 100%";
        yield return new WaitForSeconds(valueDisplayPause);

        // Determine Overall Rating
        timeStart = Time.time;
        finalRatingText.gameObject.SetActive(true);
        string resultsText = "RESULTS";
        finalRatingText.text = "";
        for (int i = 0; i < resultsText.Length; i ++)
        {
            finalRatingText.text += resultsText[i];
            yield return new WaitForSeconds(finalRatingTime / (resultsText.Length - 1));
        }

        yield return new WaitForSeconds(valueDisplayPause);
        finalRatingText.text = "Okay I guess";
    }
    private void TextSetAllActive(bool isActive)
    {
        titleText.gameObject.SetActive(isActive);
        chestsFilledText.gameObject.SetActive(isActive);
        chestQualityRatingText.gameObject.SetActive(isActive);
        thirdThingText.gameObject.SetActive(isActive);
        finalRatingText.gameObject.SetActive(isActive);
    }
}
