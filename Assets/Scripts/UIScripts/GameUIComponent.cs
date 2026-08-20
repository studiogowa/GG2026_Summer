using UnityEngine;

public abstract class GameUIComponent : MonoBehaviour
{
    public GameUI ui { get; private set; }
    protected virtual void Awake()
    {
        ui = GetComponentInParent<GameUI>();
        if (ui == null) Debug.LogError($"{this.name} IS NOT attached to a GameUI component!");
    }

    public void SetChildrenActive(bool isActive)
    {
        foreach (Transform childTransform in transform) childTransform.gameObject.SetActive(isActive);
    }
}
