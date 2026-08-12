using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Progression Variables")]
    [field: SerializeField, Range(0.0f, 360.0f)] public float duskTimeLength { get; private set; } = 120.0f;
    [field: SerializeField, Range(0.0f, 360.0f)] public float dawnTimeLength { get; private set; } = 120.0f;
    [field: SerializeField, Range(0.0f, 360.0f)] public float dayTimeLength { get; private set; } = 120.0f;

    public static GameManager instance;
    private void Awake()
    {   // Establish static reference
        if (GameManager.instance != null && GameManager.instance != this) Debug.LogError("Another GameManager tried to Instantiate! Deleting!");
        else GameManager.instance = this;
    }
    private void OnDestroy()
    {   // Remove static reference
        if (GameManager.instance != null && GameManager.instance == this) GameManager.instance = null;
    }
}
