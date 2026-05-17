using UnityEngine;
using System;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [System.Serializable]
    public class PlayerInfo
    {
        public string playerName;
        public int level = 1;
        public long experience = 0;
        public int clanId = -1;
        public Vector3 villagePosition = Vector3.zero;
    }

    [System.Serializable]
    public class Resources
    {
        public long gold = 50000;
        public long wood = 50000;
        public long stone = 50000;
        public long food = 50000;
        public long iron = 10000;
    }

    public PlayerInfo playerInfo = new PlayerInfo();
    public Resources resources = new Resources();
    public long totalGamesPlayed = 0;
    public long lastSaveTime = 0;

    private const string PLAYER_PREFS_KEY = "PlayerData_";

    public void SaveData()
    {
        lastSaveTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string json = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(PLAYER_PREFS_KEY + playerInfo.playerName, json);
        PlayerPrefs.Save();
        Debug.Log("Game saved: " + playerInfo.playerName);
    }

    public void LoadData()
    {
        if (string.IsNullOrEmpty(playerInfo.playerName))
            playerInfo.playerName = "Player_" + UnityEngine.Random.Range(1000, 9999);

        string key = PLAYER_PREFS_KEY + playerInfo.playerName;
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log("Game loaded: " + playerInfo.playerName);
        }
        else
        {
            Debug.Log("New game started");
        }
    }

    public bool AddResources(string resourceType, long amount)
    {
        switch (resourceType.ToLower())
        {
            case "gold":
                resources.gold += amount;
                break;
            case "wood":
                resources.wood += amount;
                break;
            case "stone":
                resources.stone += amount;
                break;
            case "food":
                resources.food += amount;
                break;
            case "iron":
                resources.iron += amount;
                break;
            default:
                return false;
        }
        return true;
    }

    public bool RemoveResources(string resourceType, long amount)
    {
        switch (resourceType.ToLower())
        {
            case "gold":
                if (resources.gold >= amount)
                {
                    resources.gold -= amount;
                    return true;
                }
                break;
            case "wood":
                if (resources.wood >= amount)
                {
                    resources.wood -= amount;
                    return true;
                }
                break;
            case "stone":
                if (resources.stone >= amount)
                {
                    resources.stone -= amount;
                    return true;
                }
                break;
            case "food":
                if (resources.food >= amount)
                {
                    resources.food -= amount;
                    return true;
                }
                break;
            case "iron":
                if (resources.iron >= amount)
                {
                    resources.iron -= amount;
                    return true;
                }
                break;
        }
        return false;
    }

    public void AddExperience(long amount)
    {
        experience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        long requiredExp = playerInfo.level * 1000;
        if (playerInfo.experience >= requiredExp)
        {
            playerInfo.level++;
            playerInfo.experience -= requiredExp;
            Debug.Log("Level Up! New Level: " + playerInfo.level);
        }
    }
}
