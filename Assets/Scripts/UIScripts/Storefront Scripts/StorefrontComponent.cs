using UnityEngine;

public abstract class StorefrontComponent : MonoBehaviour
{
    protected Storefront storefront { get; private set; }
    protected virtual void Awake()
    {
        storefront = GetComponentInParent<Storefront>();
        if (storefront == null) Debug.LogError("This StorefrontComponent DOES NOT have an associated Storefront in Parents");
    }
}
