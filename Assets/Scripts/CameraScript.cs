using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    public GameObject trackingTarget;
    [Range(0.0f, 10.0f)] public float trackingSpeed = 5.0f;
    private void Awake()
    {
        if (CameraScript.instance != null && CameraScript.instance != this)
        {
            Debug.LogWarning("Another Camera script tried to Initialize! Deleting!");
            Destroy(this.gameObject);
        }
        else 
        {
            CameraScript.instance = this;
        }
    }
    private void Update()
    {
        if (trackingTarget == null) return;
        Vector2 destination = Vector2.Lerp(transform.position, trackingTarget.transform.position, trackingSpeed * Time.deltaTime);
        transform.position = new Vector3(destination.x, destination.y, transform.position.z);
    }
    private void OnDestroy()
    {
        if (CameraScript.instance != null && CameraScript.instance == this) CameraScript.instance = null;
    }
}