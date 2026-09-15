using UnityEngine;

public class DroppedItems : GameManagerComponent
{
    public GameObject container { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        container = new GameObject("DROPPED ITEMS CONTAINER");
        container.transform.parent = this.transform;
    }
    private void OnEnable()
    {
        gameManager.gameEvents.performanceReviewStarts += ClearDroppedItems;
    }
    private void OnDisable()
    {
        gameManager.gameEvents.performanceReviewStarts += ClearDroppedItems;
    }
    /// <summary>
    /// Destroys every GameObject that is a child to the Dropped Items Container
    /// </summary>
    private void ClearDroppedItems()
    {
        for (int i = container.transform.childCount - 1; i >= 0; i--) Destroy(container.transform.GetChild(i).gameObject);
    }
}
