using UnityEngine;

public abstract class DungeonComponent : MonoBehaviour
{
    protected Dungeon dungeon { get; private set; }
    protected virtual void Awake()
    {
        dungeon = this.GetComponentInParent<Dungeon>();
        if (dungeon == null) Debug.LogError("No Dungeon Script found!");
    }
}
