using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float fov = 50f;
    [SerializeField] private float angle = 0;
    [SerializeField] private int rayCount = 30;


    [SerializeField] GameObject playerRef; //Testing
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool prevCanSeePlayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstructionLayer;

    private float attackRange;

    [Header("Vision Cone")]
    [SerializeField] private float rotationSpeed;
    private Vector3 aimDirection;
    //private Vector3 currAimDirection;
    private float startingAngle;
    private Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    float angleIncrease;
    int vertexIndex;
    int triangleIndex;

    public Action onPlayerInRange;
    public Action onPlayerLost;

    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player"); //Testing
        mesh = new Mesh();
        mesh.name = "Vision Cone";
        GetComponent<MeshFilter>().mesh = mesh;

        //Set FOV / Check field variables
        aimDirection = transform.up;
        SetAimDirection(transform.up);
        StartVisionCone();

        StartCoroutine(FOVCheck());
    }

    private void Update()
    {
        DrawField();
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            CheckField();
            
        }
    }

    private void CheckField()
    {
        canSeePlayer = false;

        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        //Nothing in range -> return
        if (rangeCheck.Length == 0)
        {
            UpdatePlayerStatus();
            return;
        }

        Transform target = rangeCheck[0].transform;
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // out of range -> return
        if (Vector2.Angle(aimDirection, directionToTarget) > fov / 2)
        {
            UpdatePlayerStatus();
            return;
        }
            

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // if hit obstruction -> return
        if (Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer)) return;

        // if in attack range -> attack
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("Attack!");
        }

        canSeePlayer = true;
        UpdatePlayerStatus();
        //Debug.Log("see player");
    }

    private void UpdatePlayerStatus()
    {
        // player just entered when previous can't see but now see
        if (canSeePlayer && !prevCanSeePlayer)
        {
            onPlayerInRange.Invoke();
        } // player just exited when now can't see but previous can see
        else if(!canSeePlayer && prevCanSeePlayer)
        {
            onPlayerLost.Invoke();
        }
        prevCanSeePlayer = canSeePlayer;
    }

    public void SetAimDirection(Vector3 nextDirection)
    {
        aimDirection = Vector3.RotateTowards(
            aimDirection, nextDirection.normalized,
            rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);

        startingAngle = GetAnglefromVector(aimDirection) - fov / 2f;
    }
    private void StartVisionCone()
    {
        vertices = new Vector3[rayCount + 1 + 1];
        uv = new Vector2[vertices.Length];
        triangles = new int[rayCount * 3];

        angleIncrease = fov / rayCount;
        vertexIndex = 1;
        triangleIndex = 0;
    }

    private void DrawField()
    {
        angle = startingAngle;

        vertexIndex = 1;
        triangleIndex = 0;
        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector2 direction = GetVectorFromAngle(angle);

            //If hit obstruction -> calculate distance to wall
            RaycastHit2D hitObstruction = Physics2D.Raycast(
                transform.position, 
                direction, radius, obstructionLayer);

            float distance;
            if(hitObstruction.collider == null)
            {
                distance = radius;
            } else
            {
                distance = hitObstruction.distance;
            }

            //Draw triangle depending on distance
            vertices[vertexIndex] = direction * distance;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex;
                triangles[triangleIndex + 2] = vertexIndex - 1;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle += angleIncrease;
        }

        //Draw the mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAnglefromVector(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private Vector2 DirectionFromAngle (float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }

    public void SetAttackRange(float range)
    {
        attackRange = range;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        
        //Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, startingAngle-fov / 2);
        //Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, startingAngle + fov / 2);

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        //Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        if (canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }
}


//private void DrawField()
//{
//    mesh.Clear();

//    Vector3[] vertices = new Vector3[rayCount + 1];
//    int[] triangles = new int[rayCount * 3];

//    vertices[0] = Vector3.zero;

//    float startAngle = -fov / 2;
//    float angleStep = fov / (rayCount - 1);

//    for (int i = 0; i < rayCount; i++)
//    {
//        float angle = startAngle + angleStep * i;
//        Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.up;

//        RaycastHit2D hit = Physics2D.Raycast(
//            transform.position,
//            direction,
//            radius,
//            obstructionLayer
//            );

//        float distance = hit ? hit.distance : radius;

//        Vector3 worldPoint = transform.position + (Vector3)direction * distance;

//        vertices[i + 1] = transform.InverseTransformPoint(worldPoint);
//    }

//    for (int i = 0; i < rayCount - 1; i++)
//    {
//        int triangleIndex = i * 3;

//        triangles[triangleIndex] = 0;
//        triangles[triangleIndex + 1] = i + 2;
//        triangles[triangleIndex + 2] = i + 1;
//    }

//    mesh.vertices = vertices;
//    mesh.triangles = triangles;

//    mesh.RecalculateNormals();
//    mesh.RecalculateBounds();
//    //Debug.Log($"Vertices: {mesh.vertexCount}, Triangles: {mesh.triangles.Length}");
//}

