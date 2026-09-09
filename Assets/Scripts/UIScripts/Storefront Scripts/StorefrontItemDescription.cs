using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StorefrontItemDescription : StorefrontComponent
{
    [SerializeField] private Image itemPortrait;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void ClearDescription()
    {
        itemPortrait.enabled = false;
        itemName.text = "";
        itemPrice.text = "";
        itemDescription.text = "";
    }
    public void DisplayItem(Item input)
    {
        itemPortrait.enabled = true;
        itemPortrait.sprite = input.icon;
        itemName.text = input.name;
        itemPrice.text = $"${input.value}";
        itemDescription.text = input.description;
    }
}
