using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Inventory/Potions/Health Potion")]

public class HealthPotion : Item
{
    public float healAmount;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    } 


    public override bool Use()
    {
        if (player.TryGetComponent<PlayerStats>(out PlayerStats stats))
        {
            if (stats.currentHealth < stats.maxHealth)
            {
                stats.Heal(healAmount);
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }
}
