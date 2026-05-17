using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class VillageData
{
    public int villageId;
    public string villageName;
    public int mapX;
    public int mapY;
    public int level;
    public long totalProduction; // İstatistik

    [System.Serializable]
    public class Storage
    {
        public long gold;
        public long wood;
        public long food;
        public long maxCapacity = 10000;

        public bool CanStore(ResourceType type, long amount)
        {
            return (GetTotal() + amount) <= maxCapacity;
        }

        public long GetTotal() => gold + wood + food;
    }

    public Storage storage = new Storage();
    public List<Building> buildings = new List<Building>();
    public List<Unit> units = new List<Unit>();

    public void Initialize(string name, int id, int x, int y)
    {
        villageId = id;
        villageName = name;
        mapX = x;
        mapY = y;
        level = 1;
        totalProduction = 0;

        // Varsayılan binalar
        CreateDefaultBuildings();
    }

    private void CreateDefaultBuildings()
    {
        // Ana Merkez
        buildings.Add(new Building
        {
            buildingId = 0,
            buildingType = BuildingType.MainHall,
            level = 1,
            gridX = 5,
            gridY = 5
        });

        // Barracks
        buildings.Add(new Building
        {
            buildingId = 1,
            buildingType = BuildingType.Barracks,
            level = 1,
            gridX = 3,
            gridY = 5
        });

        // Depo
        buildings.Add(new Building
        {
            buildingId = 2,
            buildingType = BuildingType.Storage,
            level = 1,
            gridX = 7,
            gridY = 5
        });
    }
}

[System.Serializable]
public class Building
{
    public int buildingId;
    public BuildingType buildingType;
    public int level;
    public int gridX;
    public int gridY;
    public long constructionStartTime;
    public long constructionDuration; // saniye
    public long lastUpgradeTime;

    public bool IsConstructionComplete(long currentTime)
    {
        return (currentTime - constructionStartTime) >= constructionDuration;
    }

    public float GetConstructionProgress(long currentTime)
    {
        long elapsed = currentTime - constructionStartTime;
        return Mathf.Clamp01((float)elapsed / constructionDuration);
    }
}

[System.Serializable]
public class Unit
{
    public int unitId;
    public UnitType unitType;
    public int level;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int defensePower;
}

public enum BuildingType
{
    MainHall,      // Merkez
    Barracks,      // Kışla
    Storage,       // Depo
    GoldMine,      // Altın Madeni
    LumberMill,    // Kereste Fabrikası
    Farm,          // Çiftlik
    Tower,         // Savunma Kulesi
    Wall,          // Duvar
    Laboratory,    // Laboratuvar (araştırma)
    Dock           // İskele (ticaret)
}

public enum UnitType
{
    Warrior,       // Savaşçı
    Archer,        // Okçu
    Cavalry,       // Süvari
    Siege,         // Muhasara Makinesi
    Scout          // İzciler
}
