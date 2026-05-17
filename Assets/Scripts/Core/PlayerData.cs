using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerId;
    public string playerName;
    public int level;
    public long totalExperience;

    [System.Serializable]
    public class Resources
    {
        public long gold;
        public long wood;
        public long food;
        public long gems; // Premium para birimi
    }

    public Resources resources = new Resources();

    [System.Serializable]
    public class AdStats
    {
        public long lastAdWatchTime; // Unix timestamp
        public int adWatchCountToday;
        public long dailyAdResetTime; // Unix timestamp
    }

    public AdStats adStats = new AdStats();

    public List<VillageData> villages = new List<VillageData>();
    public ClanData clan;
    public List<int> ownedTerritories = new List<int>(); // Territory IDs

    public void Initialize()
    {
        playerId = System.Guid.NewGuid().ToString();
        playerName = "Oyuncu_" + Random.Range(1000, 9999);
        level = 1;
        totalExperience = 0;

        resources.gold = 1000;
        resources.wood = 500;
        resources.food = 750;
        resources.gems = 0;

        adStats.lastAdWatchTime = 0;
        adStats.adWatchCountToday = 0;
        adStats.dailyAdResetTime = GetCurrentUnixTime() + (24 * 3600);

        // İlk köyü oluştur
        VillageData firstVillage = new VillageData();
        firstVillage.Initialize("Ana Köy", 0, 50, 50);
        villages.Add(firstVillage);
    }

    public long GetCurrentUnixTime()
    {
        return System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public bool AddResource(ResourceType type, long amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                resources.gold += amount;
                return true;
            case ResourceType.Wood:
                resources.wood += amount;
                return true;
            case ResourceType.Food:
                resources.food += amount;
                return true;
            case ResourceType.Gems:
                resources.gems += amount;
                return true;
        }
        return false;
    }

    public bool RemoveResource(ResourceType type, long amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                if (resources.gold >= amount)
                {
                    resources.gold -= amount;
                    return true;
                }
                break;
            case ResourceType.Wood:
                if (resources.wood >= amount)
                {
                    resources.wood -= amount;
                    return true;
                }
                break;
            case ResourceType.Food:
                if (resources.food >= amount)
                {
                    resources.food -= amount;
                    return true;
                }
                break;
            case ResourceType.Gems:
                if (resources.gems >= amount)
                {
                    resources.gems -= amount;
                    return true;
                }
                break;
        }
        return false;
    }
}

public enum ResourceType
{
    Gold,
    Wood,
    Food,
    Gems
}
