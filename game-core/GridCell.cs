using UnityEngine;

public class GridCell
{
    public int X { get; set; }
    public int Y { get; set; }
    public Vector3 WorldPosition { get; set; }
    public TerrainType Terrain { get; set; }
    public int ResourceAmount { get; set; }

    public GridCell(int x, int y, Vector3 worldPos, TerrainType terrain = TerrainType.Grass)
    {
        X = x;
        Y = y;
        WorldPosition = worldPos;
        Terrain = terrain;
        ResourceAmount = 0;
    }
}

public enum TerrainType
{
    Grass,
    Forest,
    Mountain,
    Water,
    Desert
}
