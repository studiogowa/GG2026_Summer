using UnityEngine;
using UnityEngine.Tilemaps;
public class DungeonTilemap : DungeonComponent
{
    private Tilemap floorTilemap;
    private Tilemap wallTilemap;
    private Tilemap minimapTilemap;
    protected override void Awake()
    {
        base.Awake();
        if (!transform.GetChild(0).TryGetComponent<Tilemap>(out floorTilemap)) Debug.LogError("This Dungeon DOES NOT have a floor Tilemap!");
        if (!transform.GetChild(1).TryGetComponent<Tilemap>(out wallTilemap)) Debug.LogError("This Dungeon DOES NOT have a wall Tilemap!");
        if (!transform.GetChild(2).TryGetComponent<Tilemap>(out minimapTilemap)) Debug.LogError("This Dungeon DOES NOT have a minimap Tilemap!");

        CreateMinimapTilemap();
    }
    private void CreateMinimapTilemap()
    {
        if (wallTilemap == null || minimapTilemap == null) return;
        minimapTilemap.ClearAllTiles();
        minimapTilemap.SetTilesBlock(wallTilemap.cellBounds, wallTilemap.GetTilesBlock(wallTilemap.cellBounds));
    }
}
