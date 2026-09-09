using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class Storefront : MonoBehaviour
{
    [SerializeField] private ItemPool shopItemPool;
    [SerializeField] private Animator storeAnimator;
    [SerializeField] private Animator storeManagerAnimator;

    public List<StorefrontItem> storefrontItems;

    [SerializeField] private Item focusedItem;
    [SerializeField] private Button buyButton;

    [SerializeField] StorefrontItemDescription description;

    [SerializeField] private bool isOpen = false;
    private void OnEnable()
    {
        buyButton.onClick.AddListener(BuyItem);
    }
    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(BuyItem);
    }
    private void Update()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame) ToggleShop();
    }
    private void ToggleShop()
    {
        if (!isOpen) OpenShop();
        else CloseShop();
    }
    private void OpenShop()
    {
        SetUpShop();
        storeAnimator.SetTrigger("Open");
        isOpen = true;
    }
    private void CloseShop()
    {
        storeAnimator.SetTrigger("Close");
        isOpen = false;
    }
    private void SetUpShop()
    {
        GenerateShopItems();
        buyButton.gameObject.SetActive(false);

        description.ClearDescription();
    }
    private void GenerateShopItems()
    {
        Item[] shopPoolItems = new Item[shopItemPool.itemPoolItems.Count];
        int index = 0;
        foreach (ItemPoolItem currItem in shopItemPool.itemPoolItems) shopPoolItems[index++] = currItem.item;

        ArrayHelpers.ShuffleArray<Item>(shopPoolItems);

        index = 0;
        foreach (StorefrontItem currDisplay in storefrontItems) currDisplay.DisplayItem(shopPoolItems[index++]);
    }
    public void FocusItem(Item input)
    {
        buyButton.gameObject.SetActive(true);
        focusedItem = input;
        description.DisplayItem(input);
    }
    private void BuyItem()
    {
        // Check if player has enough money

        PlayerInventory.instance.Add(focusedItem, 1);
    }
}
