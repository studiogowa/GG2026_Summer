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

    [HideInInspector] public List<StorefrontItem> storefrontItems;

    [SerializeField] private Item focusedItem;
    [SerializeField] private Button buyButton;

    [SerializeField] StorefrontItemDescription description;

    private bool isOpen = false;

    [SerializeField] private Button exitButton;

    [SerializeField] StorefrontPlayerFunds playerFunds;
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
        StopAllCoroutines();
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
        isOpen = false;
        storeManagerAnimator.SetTrigger("Bow");
        yield return new WaitForSeconds(1.5f);
        storeAnimator.SetTrigger("Close");
    }
    
    private void SetUpShop()
    {
        GenerateShopItems();
        buyButton.gameObject.SetActive(false);

        description.ClearDescription();
        playerFunds.UpdateFunds();
    }
    /// <summary>
    /// Performs the sequence the Store Manager animations while the player is shopping
    /// </summary>
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
            if (Random.Range(0, 3) == 0)
            {
                yield return new WaitForSeconds(0.1f);
                storeManagerAnimator.SetTrigger("FlipPage");
            }
        }
    }
    /// <summary>
    /// Chooses 5 randoms items in the shop item pool to sell, and puts them up for display
    /// </summary>
    private void GenerateShopItems()
    {
        Item[] shopPoolItems = new Item[shopItemPool.itemPoolItems.Count];
        int index = 0;
        foreach (ItemPoolItem currItem in shopItemPool.itemPoolItems) shopPoolItems[index++] = currItem.item;

        ArrayHelpers.ShuffleArray<Item>(shopPoolItems);

        index = 0;
        foreach (StorefrontItem currDisplay in storefrontItems) currDisplay.DisplayItem(shopPoolItems[index++]);
    }
    /// <summary>
    /// Assigned the input Item as the item to be displayed in more detail
    /// </summary>
    /// <param name="input">The item to describe further</param>
    public void FocusItem(Item input)
    {
        focusedItem = input;
        description.DisplayItem(input);
        buyButton.gameObject.SetActive(true);
        // Enable button interactivity only if player can AND carry the focused item 
        if (GameManager.instance.playerFunds.SubtractableBy(input.value) && PlayerInventory.instance.IsAddable(input, 1)) buyButton.interactable = true;
        else buyButton.interactable = false;
    }
    private void BuyItem()
    {
        // Check if player has enough money
        if (!GameManager.instance.playerFunds.SubtractableBy(focusedItem.value)) return;
        // If player CAN'T add the item bought
        if (!PlayerInventory.instance.Add(focusedItem, 1)) return;
        
        GameManager.instance.playerFunds.SubtractFunds(focusedItem.value);
        // Update item description
        FocusItem(focusedItem);
        playerFunds.UpdateFunds();
    }
}
