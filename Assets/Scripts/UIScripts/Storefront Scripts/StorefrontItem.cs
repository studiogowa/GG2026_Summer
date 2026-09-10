using UnityEngine;
using UnityEngine.UI;
public class StorefrontItem : StorefrontComponent
{
    private Button button;
    [SerializeField] private Image itemPortrait;
    [SerializeField] private Item item;
    protected override void Awake()
    {
        base.Awake();
        storefront.storefrontItems.Add(this);
        if (!TryGetComponent<Button>(out button)) Debug.LogError("This GameObject has no Button Component!");
    }
    private void OnEnable()
    {
        button.onClick.AddListener(FocusItem);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(FocusItem);
    }
    public void DisplayItem(Item input)
    {
        item = input;
        itemPortrait.sprite = input.icon;
    }
    private void FocusItem()
    {
        if (item == null) return;

        storefront.FocusItem(item);
    }
}
