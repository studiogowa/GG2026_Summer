using UnityEngine;

[CreateAssetMenu(fileName = "Speed Potion", menuName = "Inventory/Speed Potion")]
public class SpeedPotion : Item
{
    public float speedBoost;
    public float duration;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    } 


    public override void Use()
    {
        if (player.TryGetComponent<PlayerStats>(out PlayerStats stats))
        {
            stats.StartCoroutine(stats.TempSpeedChange(speedBoost, duration));
        }
    }
}
