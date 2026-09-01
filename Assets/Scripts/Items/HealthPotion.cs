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


    public override void Use()
    {
        if (player.TryGetComponent<PlayerStats>(out PlayerStats stats))
        {
            stats.Heal(healAmount);
        }
    }
}
