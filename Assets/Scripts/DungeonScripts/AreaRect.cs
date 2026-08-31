using UnityEngine;

public class AreaRect : MonoBehaviour
{
    [SerializeField] private float xLength = 0;
    [SerializeField] private float yLength = 0;

    [SerializeField] private Color gizmoColor = Color.green;
    public Rect areaRect { get { return new Rect(transform.position - new Vector3(xLength/2, yLength/2), new Vector2(xLength, yLength)); } }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 center = new Vector3(transform.position.x, transform.position.y, 0.0f);
        Vector3 size = new Vector3(xLength, yLength, 0.01f);

        Gizmos.DrawWireCube(center, size);
    }
}