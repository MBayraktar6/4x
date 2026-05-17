using UnityEngine;
using System.Collections.Generic;

public static class BuildingDatabase
{
    private static Dictionary<BuildingType, BuildingConfig> configs = new Dictionary<BuildingType, BuildingConfig>();

    static BuildingDatabase()
    {
        InitializeConfigs();
    }

    private static void InitializeConfigs()
    {
        configs[BuildingType.MainHall] = new BuildingConfig
        {
            buildingType = BuildingType.MainHall,
            buildingName = "Ana Merkez",
            goldCost = 0,
            woodCost = 0,
            foodCost = 0,
            constructionTimeSeconds = 0,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 10
        };

        configs[BuildingType.Barracks] = new BuildingConfig
        {
            buildingType = BuildingType.Barracks,
            buildingName = "Kışla",
            goldCost = 500,
            woodCost = 800,
            foodCost = 200,
            constructionTimeSeconds = 120,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 5
        };

        configs[BuildingType.Storage] = new BuildingConfig
        {
            buildingType = BuildingType.Storage,
            buildingName = "Depo",
            goldCost = 300,
            woodCost = 600,
            foodCost = 100,
            constructionTimeSeconds = 90,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 2
        };

        configs[BuildingType.GoldMine] = new BuildingConfig
        {
            buildingType = BuildingType.GoldMine,
            buildingName = "Altın Madeni",
            goldCost = 400,
            woodCost = 500,
            foodCost = 300,
            constructionTimeSeconds = 150,
            goldProduction = 10,
            woodProduction = 0,
            foodProduction = 0,
            defense = 1
        };

        configs[BuildingType.LumberMill] = new BuildingConfig
        {
            buildingType = BuildingType.LumberMill,
            buildingName = "Kereste Fabrikası",
            goldCost = 300,
            woodCost = 200,
            foodCost = 200,
            constructionTimeSeconds = 120,
            goldProduction = 0,
            woodProduction = 15,
            foodProduction = 0,
            defense = 1
        };

        configs[BuildingType.Farm] = new BuildingConfig
        {
            buildingType = BuildingType.Farm,
            buildingName = "Çiftlik",
            goldCost = 200,
            woodCost = 300,
            foodCost = 100,
            constructionTimeSeconds = 100,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 12,
            defense = 0
        };

        configs[BuildingType.Tower] = new BuildingConfig
        {
            buildingType = BuildingType.Tower,
            buildingName = "Savunma Kulesi",
            goldCost = 600,
            woodCost = 700,
            foodCost = 200,
            constructionTimeSeconds = 180,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 20
        };

        configs[BuildingType.Wall] = new BuildingConfig
        {
            buildingType = BuildingType.Wall,
            buildingName = "Duvar",
            goldCost = 200,
            woodCost = 400,
            foodCost = 50,
            constructionTimeSeconds = 60,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 8
        };

        configs[BuildingType.Laboratory] = new BuildingConfig
        {
            buildingType = BuildingType.Laboratory,
            buildingName = "Laboratuvar",
            goldCost = 800,
            woodCost = 900,
            foodCost = 400,
            constructionTimeSeconds = 240,
            goldProduction = 0,
            woodProduction = 0,
            foodProduction = 0,
            defense = 3
        };

        configs[BuildingType.Dock] = new BuildingConfig
        {
            buildingType = BuildingType.Dock,
            buildingName = "İskele",
            goldCost = 500,
            woodCost = 1000,
            foodCost = 300,
            constructionTimeSeconds = 200,
            goldProduction = 5,
            woodProduction = 5,
            foodProduction = 5,
            defense = 2
        };
    }

    public static BuildingConfig GetConfig(BuildingType type)
    {
        configs.TryGetValue(type, out var config);
        return config;
    }
}

[System.Serializable]
public class BuildingConfig
{
    public BuildingType buildingType;
    public string buildingName;
    public long goldCost;
    public long woodCost;
    public long foodCost;
    public long constructionTimeSeconds;
    public long goldProduction;
    public long woodProduction;
    public long foodProduction;
    public int defense;

    public long GetUpgradeCost(int currentLevel)
    {
        // Her seviye için 20% daha fazla maliyet
        return (long)(goldCost * Mathf.Pow(1.2f, currentLevel));
    }
}
