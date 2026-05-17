using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField] private int gridWidth = 10;
    [SerializeField] private int gridHeight = 10;
    [SerializeField] private float cellSize = 1f;

    private GridCell[,] grid;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void InitializeGrid()
    {
        grid = new GridCell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPos = new Vector3(x * cellSize, 0, y * cellSize);
                TerrainType terrain = Random.value > 0.7f ? TerrainType.Forest : TerrainType.Grass;
                grid[x, y] = new GridCell(x, y, worldPos, terrain);
            }
        }
    }

    public GridCell GetCell(int x, int y)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            return grid[x, y];
        return null;
    }

    public List<GridCell> GetAdjacentCells(int x, int y)
    {
        List<GridCell> adjacent = new List<GridCell>();
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                GridCell cell = GetCell(x + dx, y + dy);
                if (cell != null) adjacent.Add(cell);
            }
        }
        return adjacent;
    }
}
