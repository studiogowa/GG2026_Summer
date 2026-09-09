using UnityEngine;

public class PlayerFunds : MonoBehaviour
{
    [field: SerializeField, Range(0, 500)] public int funds { get; private set; } = 0;

    public void AddFunds(int amount)
    {
        funds += amount;
    }
    public void SubtractFunds(int amount)
    {
        funds -= amount;
    }
    public bool SubtractableBy(int amount)
    {
        if (funds - amount >= 0) return true;
        else return false;
    }
}
