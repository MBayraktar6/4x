using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ClanData
{
    public int clanId;
    public string clanName;
    public string description;
    public string leaderPlayerId;
    public int level;
    public long totalClanExp;
    public int maxMembers = 50;

    [System.Serializable]
    public class ClanTreasury
    {
        public long gold;
        public long wood;
        public long food;
    }

    public ClanTreasury treasury = new ClanTreasury();
    public List<string> members = new List<string>();
    public List<int> ownedTerritories = new List<int>();
    public List<ClanTechnology> technologies = new List<ClanTechnology>();

    public void Initialize(string name, string leaderId, string desc)
    {
        clanId = Random.Range(100000, 999999);
        clanName = name;
        description = desc;
        leaderPlayerId = leaderId;
        level = 1;
        totalClanExp = 0;

        members.Add(leaderId);
    }

    public void AddMember(string playerId)
    {
        if (!members.Contains(playerId) && members.Count < maxMembers)
        {
            members.Add(playerId);
        }
    }

    public void RemoveMember(string playerId)
    {
        members.Remove(playerId);
    }

    public bool AddToTreasury(ResourceType type, long amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                treasury.gold += amount;
                return true;
            case ResourceType.Wood:
                treasury.wood += amount;
                return true;
            case ResourceType.Food:
                treasury.food += amount;
                return true;
        }
        return false;
    }

    public bool RemoveFromTreasury(ResourceType type, long amount)
    {
        switch (type)
        {
            case ResourceType.Gold:
                if (treasury.gold >= amount)
                {
                    treasury.gold -= amount;
                    return true;
                }
                break;
            case ResourceType.Wood:
                if (treasury.wood >= amount)
                {
                    treasury.wood -= amount;
                    return true;
                }
                break;
            case ResourceType.Food:
                if (treasury.food >= amount)
                {
                    treasury.food -= amount;
                    return true;
                }
                break;
        }
        return false;
    }
}

[System.Serializable]
public class ClanTechnology
{
    public int technologyId;
    public string technologyName;
    public int level;
    public float effectiveness; // 0-1 between
}
