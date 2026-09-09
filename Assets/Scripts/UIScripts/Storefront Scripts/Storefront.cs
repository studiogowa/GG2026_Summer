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

    private bool isOpen = false;

    [SerializeField] private Button exitButton;
    private void OnEnable()
    {
        buyButton.onClick.AddListener(BuyItem);
        exitButton.onClick.AddListener(CloseShop);
    }
    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(BuyItem);
        exitButton.onClick.RemoveListener(CloseShop);
    }
    private void Update()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame) OpenShop();
    }
    private void ToggleShop()
    {
        if (!isOpen) OpenShop();
        else CloseShop();
    }
    private void OpenShop()
    {
        if (isOpen) return;
        SetUpShop();
        storeAnimator.SetTrigger("Open");
        StartCoroutine(StoreManagerCoroutine());
        isOpen = true;
    }
    private void CloseShop()
    {
        if (!isOpen) return;
        StopAllCoroutines();
        StartCoroutine(CloseShopCoroutine());
    }
    private IEnumerator CloseShopCoroutine()
    {
        storeManagerAnimator.SetTrigger("Bow");
        yield return new WaitForSeconds(1.5f);
        storeAnimator.SetTrigger("Close");
        isOpen = false;
    }
    private void SetUpShop()
    {
        GenerateShopItems();
        buyButton.gameObject.SetActive(false);

        description.ClearDescription();
    }
    private IEnumerator StoreManagerCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        storeManagerAnimator.SetTrigger("FlipPage");
        yield return new WaitForSeconds(0.1f);
        storeManagerAnimator.SetTrigger("FlipPage");
        yield return new WaitForSeconds(1.0f);
        storeManagerAnimator.SetTrigger("RaiseHead");
        yield return new WaitForSeconds(5.0f);
        storeManagerAnimator.SetTrigger("LowerHead");
        yield return new WaitForSeconds(0.25f);
        storeManagerAnimator.SetTrigger("FlipPage");
        yield return new WaitForSeconds(0.1f);
        storeManagerAnimator.SetTrigger("FlipPage");
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6, 8));
            storeManagerAnimator.SetTrigger("FlipPage");
        }
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
