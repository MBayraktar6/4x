using UnityEngine;
using System.Collections.Generic;

public class ClanManager : MonoBehaviour
{
    [System.Serializable]
    public class Clan
    {
        public int clanId;
        public string clanName;
        public int leaderId;
        public List<int> memberIds = new List<int>();
        public long clanGold = 0;
        public int level = 1;
        public long experience = 0;
        public Vector3 clanHallPosition;
        public List<Vector2Int> ownedTerritories = new List<Vector2Int>();
    }

    public Clan playerClan;
    public Dictionary<int, Clan> allClans = new Dictionary<int, Clan>();
    private int nextClanId = 1;

    public Clan CreateClan(string clanName, int leaderId)
    {
        Clan newClan = new Clan
        {
            clanId = nextClanId++,
            clanName = clanName,
            leaderId = leaderId,
            level = 1,
            experience = 0,
            clanGold = 10000
        };

        newClan.memberIds.Add(leaderId);
        allClans.Add(newClan.clanId, newClan);
        playerClan = newClan;

        GameManager.Instance.playerData.playerInfo.clanId = newClan.clanId;

        Debug.Log("Clan created: " + clanName);
        return newClan;
    }

    public void JoinClan(int playerId, int clanId)
    {
        if (allClans.ContainsKey(clanId))
        {
            allClans[clanId].memberIds.Add(playerId);
            Debug.Log("Player " + playerId + " joined clan " + clanId);
        }
    }

    public void LeaveClan(int playerId, int clanId)
    {
        if (allClans.ContainsKey(clanId))
        {
            allClans[clanId].memberIds.Remove(playerId);
            Debug.Log("Player " + playerId + " left clan " + clanId);
        }
    }

    public void AddClanExperience(int clanId, long amount)
    {
        if (allClans.ContainsKey(clanId))
        {
            Clan clan = allClans[clanId];
            clan.experience += amount;
            CheckClanLevelUp(clan);
        }
    }

    private void CheckClanLevelUp(Clan clan)
    {
        long requiredExp = clan.level * 10000;
        if (clan.experience >= requiredExp)
        {
            clan.level++;
            clan.experience -= requiredExp;
            Debug.Log("Clan " + clan.clanName + " leveled up! New level: " + clan.level);
        }
    }

    public void UpdateClan(float deltaTime)
    {
        // Clan updates
    }

    public void SaveClanData()
    {
        // Implement save logic
    }

    public void LoadClanData()
    {
        // Implement load logic
    }
}
