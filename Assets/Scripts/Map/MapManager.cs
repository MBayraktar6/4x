using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [System.Serializable]
    public class Tile
    {
        public Vector2Int position;
        public TileType tileType;
        public int clanId = -1; // -1 means neutral
        public Building building;
        public List<Unit> units = new List<Unit>();
    }

    public enum TileType
    {
        Grass,
        Forest,
        Mountain,
        Water,
        Desert
    }

    [Header("Map Settings")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    public float tileSize = 1f;

    private Tile[,] mapGrid;
    private Dictionary<Vector2Int, GameObject> tileGameObjects = new Dictionary<Vector2Int, GameObject>();

    public void InitializeMap()
    {
        mapGrid = new Tile[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                mapGrid[x, y] = new Tile
                {
                    position = new Vector2Int(x, y),
                    tileType = GenerateRandomTile(),
                    clanId = -1
                };
            }
        }

        Debug.Log("Map initialized: " + mapWidth + " x " + mapHeight);
    }

    private TileType GenerateRandomTile()
    {
        float random = Random.value;
        if (random < 0.5f) return TileType.Grass;
        else if (random < 0.7f) return TileType.Forest;
        else if (random < 0.85f) return TileType.Mountain;
        else if (random < 0.95f) return TileType.Water;
        else return TileType.Desert;
    }

    public Tile GetTile(Vector2Int position)
    {
        if (IsValidPosition(position))
        {
            return mapGrid[position.x, position.y];
        }
        return null;
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < mapWidth && position.y >= 0 && position.y < mapHeight;
    }

    public bool CanPlaceBuilding(Vector2Int position)
    {
        Tile tile = GetTile(position);
        if (tile == null) return false;
        if (tile.building != null) return false;
        if (tile.tileType == TileType.Water) return false;
        return true;
    }

    public void ClaimTile(Vector2Int position, int clanId)
    {
        Tile tile = GetTile(position);
        if (tile != null)
        {
            tile.clanId = clanId;
        }
    }

    public List<Tile> GetClanTerritories(int clanId)
    {
        List<Tile> territories = new List<Tile>();
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (mapGrid[x, y].clanId == clanId)
                    territories.Add(mapGrid[x, y]);
            }
        }
        return territories;
    }

    public void SaveMapData()
    {
        // Implement save logic
    }

    public void LoadMapData()
    {
        // Implement load logic
    }
}
