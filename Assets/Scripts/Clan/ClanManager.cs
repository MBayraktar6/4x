using UnityEngine;
using System.Collections.Generic;

public class ClanManager : MonoBehaviour
{
    private PlayerData playerData;
    private List<ClanData> allClans = new List<ClanData>();

    public void Initialize(PlayerData data)
    {
        playerData = data;
        Debug.Log("[ClanManager] Klan sistemi başlatıldı.");
    }

    public bool CreateClan(string clanName, string description)
    {
        if (playerData.clan != null)
        {
            Debug.LogWarning("[ClanManager] Zaten bir klana üyesiniz!");
            return false;
        }

        ClanData newClan = new ClanData();
        newClan.Initialize(clanName, playerData.playerId, description);
        playerData.clan = newClan;
        allClans.Add(newClan);

        Debug.Log($"[ClanManager] Yeni klan oluşturuldu: {clanName}");
        return true;
    }

    public bool JoinClan(int clanId)
    {
        var clan = allClans.Find(c => c.clanId == clanId);
        if (clan == null)
            return false;

        if (playerData.clan != null)
        {
            Debug.LogWarning("[ClanManager] Zaten bir klana üyesiniz!");
            return false;
        }

        if (clan.members.Count >= clan.maxMembers)
        {
            Debug.LogWarning("[ClanManager] Klan dolu!");
            return false;
        }

        clan.AddMember(playerData.playerId);
        playerData.clan = clan;

        Debug.Log($"[ClanManager] {playerData.playerName} klana katıldı: {clan.clanName}");
        return true;
    }

    public bool ClaimTerritory(int territoryId)
    {
        if (playerData.clan == null)
        {
            Debug.LogWarning("[ClanManager] Bölge hakkı talep etmek için klana ait olmalısınız!");
            return false;
        }

        if (playerData.ownedTerritories.Contains(territoryId))
        {
            Debug.LogWarning("[ClanManager] Bu bölge zaten sahip olunuyor!");
            return false;
        }

        playerData.ownedTerritories.Add(territoryId);
        playerData.clan.ownedTerritories.Add(territoryId);

        Debug.Log($"[ClanManager] Bölge ele geçirildi: {territoryId}");
        return true;
    }

    public ClanData GetClan(int clanId)
    {
        return allClans.Find(c => c.clanId == clanId);
    }

    public List<ClanData> GetAllClans()
    {
        return new List<ClanData>(allClans);
    }
}
