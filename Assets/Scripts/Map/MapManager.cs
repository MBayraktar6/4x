using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    private GameConfig config;
    private Tile[,] tiles;
    private List<Territory> territories = new List<Territory>();

    [System.Serializable]
    public class Tile
    {
        public int x;
        public int y;
        public TerrainType terrainType;
        public int territoryId = -1; // Hangi bölgeye ait
        public int ownerId = -1; // Oyuncu ID
    }

    [System.Serializable]
    public class Territory
    {
        public int territoryId;
        public int centerX;
        public int centerY;
        public int size; // Kaç tile
        public int controllingPlayerId = -1;
        public List<Tile> tiles = new List<Tile>();
    }

    public void Initialize(GameConfig gameConfig)
    {
        config = gameConfig;
        GenerateMap();
        GenerateTerritories();
        Debug.Log($"[MapManager] Harita oluşturuldu: {config.mapWidth}x{config.mapHeight}");
    }

    private void GenerateMap()
    {
        tiles = new Tile[config.mapWidth, config.mapHeight];

        for (int x = 0; x < config.mapWidth; x++)
        {
            for (int y = 0; y < config.mapHeight; y++)
            {
                tiles[x, y] = new Tile
                {
                    x = x,
                    y = y,
                    terrainType = RandomTerrainType()
                };
            }
        }
    }

    private void GenerateTerritories()
    {
        int territorySize = 10; // 10x10 bölgeler
        int id = 0;

        for (int x = 0; x < config.mapWidth; x += territorySize)
        {
            for (int y = 0; y < config.mapHeight; y += territorySize)
            {
                Territory territory = new Territory
                {
                    territoryId = id++,
                    centerX = x + territorySize / 2,
                    centerY = y + territorySize / 2,
                    size = territorySize
                };

                // Bölgeyi tile'larla doldur
                for (int tx = x; tx < x + territorySize && tx < config.mapWidth; tx++)
                {
                    for (int ty = y; ty < y + territorySize && ty < config.mapHeight; ty++)
                    {
                        tiles[tx, ty].territoryId = territory.territoryId;
                        territory.tiles.Add(tiles[tx, ty]);
                    }
                }

                territories.Add(territory);
            }
        }
    }

    private TerrainType RandomTerrainType()
    {
        float rand = Random.value;
        if (rand < 0.5f) return TerrainType.Grass;
        if (rand < 0.7f) return TerrainType.Forest;
        if (rand < 0.85f) return TerrainType.Mountain;
        return TerrainType.Water;
    }

    public Tile GetTile(int x, int y)
    {
        if (x >= 0 && x < config.mapWidth && y >= 0 && y < config.mapHeight)
            return tiles[x, y];
        return null;
    }

    public Territory GetTerritory(int territoryId)
    {
        if (territoryId >= 0 && territoryId < territories.Count)
            return territories[territoryId];
        return null;
    }

    public List<Territory> GetAllTerritories() => new List<Territory>(territories);

    public bool ClaimTerritory(int playerId, int territoryId)
    {
        var territory = GetTerritory(territoryId);
        if (territory == null)
            return false;

        if (territory.controllingPlayerId != -1)
        {
            Debug.LogWarning("[MapManager] Bu bölge zaten kontrol ediliyor!");
            return false;
        }

        territory.controllingPlayerId = playerId;
        foreach (var tile in territory.tiles)
        {
            tile.ownerId = playerId;
        }

        Debug.Log($"[MapManager] Oyuncu {playerId} bölge {territoryId} ele geçirdi.");
        return true;
    }
}

public enum TerrainType
{
    Grass,
    Forest,
    Mountain,
    Water
}
