using UnityEngine;
using UnityEngine.AI;

public class NavMeshRandomPoint : MonoBehaviour
{
    public bool TryGetRandomPoint(Vector3 npcPosition, float radius, out Vector3 destination)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        Vector3 randomTarget = new Vector3(
            npcPosition.x + randomPoint.x,
            npcPosition.y + randomPoint.y,
            npcPosition.z - 0.08f
        );

        if (NavMesh.SamplePosition(
            randomTarget,
            out NavMeshHit hit,
            radius,
            NavMesh.AllAreas))
        {
            destination = hit.position;
            return true;
        }

        destination = npcPosition;
        return false;
    }
}
