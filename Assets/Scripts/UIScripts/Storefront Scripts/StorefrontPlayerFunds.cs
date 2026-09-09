using UnityEngine;
using TMPro;
public class StorefrontPlayerFunds : StorefrontComponent
{
    [SerializeField] private TextMeshProUGUI fundsDisplay;

    public void UpdateFunds()
    {
        fundsDisplay.text = $"${GameManager.instance.playerFunds.funds}";
    }
}
