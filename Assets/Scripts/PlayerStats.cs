using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] public float movementSpeed = 5.0f;
    public float healthPoints;

    public IEnumerator TempSpeedChange(float multiplier, float duration)
    {
        movementSpeed *= multiplier;
        Debug.Log("Movement speed is " + movementSpeed);
        yield return new WaitForSeconds(duration);
        movementSpeed /= multiplier;
        Debug.Log("Movement speed is " + movementSpeed);
    }
}
