using UnityEngine;
using System;

public class OfflineProgressSystem : MonoBehaviour
{
    public static OfflineProgressSystem Instance { get; private set; }

    [Header("Offline Settings")]
    public bool offlineProgressEnabled = true;
    public float offlineGainMultiplier = 0.5f; // 50% of normal production
    public int maxOfflineHours = 12; // Max 12 hours of offline progress

    private DateTime lastGameExitTime;
    private DateTime lastGameOpenTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CalculateOfflineGains();
    }

    public void OnGameExit()
    {
        lastGameExitTime = DateTime.UtcNow;
        PlayerPrefs.SetString("LastExitTime", lastGameExitTime.ToString());
    }

    private void CalculateOfflineGains()
    {
        if (!offlineProgressEnabled)
            return;

        string exitTimeStr = PlayerPrefs.GetString("LastExitTime", "");
        if (string.IsNullOrEmpty(exitTimeStr))
            return;

        DateTime exitTime = DateTime.Parse(exitTimeStr);
        TimeSpan offlineTime = DateTime.UtcNow - exitTime;

        // Clamp to max offline hours
        if (offlineTime.TotalHours > maxOfflineHours)
            offlineTime = TimeSpan.FromHours(maxOfflineHours);

        // Calculate resource production during offline time
        long offlineHours = (long)offlineTime.TotalHours;
        if (offlineHours > 0)
        {
            ApplyOfflineGains(offlineHours);
        }
    }

    private void ApplyOfflineGains(long offlineHours)
    {
        PlayerData playerData = GameManager.Instance.playerData;
        BuildingManager buildingManager = GameManager.Instance.buildingManager;

        long totalGold = 0;
        long totalWood = 0;
        long totalStone = 0;
        long totalFood = 0;
        long totalIron = 0;

        // Calculate production from all buildings
        foreach (Building building in buildingManager.buildings)
        {
            totalGold += building.productionPerHour.gold * offlineHours;
            totalWood += building.productionPerHour.wood * offlineHours;
            totalStone += building.productionPerHour.stone * offlineHours;
            totalFood += building.productionPerHour.food * offlineHours;
            totalIron += building.productionPerHour.iron * offlineHours;
        }

        // Apply multiplier for offline progress (reduce gains)
        totalGold = (long)(totalGold * offlineGainMultiplier);
        totalWood = (long)(totalWood * offlineGainMultiplier);
        totalStone = (long)(totalStone * offlineGainMultiplier);
        totalFood = (long)(totalFood * offlineGainMultiplier);
        totalIron = (long)(totalIron * offlineGainMultiplier);

        // Add resources
        playerData.AddResources("gold", totalGold);
        playerData.AddResources("wood", totalWood);
        playerData.AddResources("stone", totalStone);
        playerData.AddResources("food", totalFood);
        playerData.AddResources("iron", totalIron);

        NotificationSystem.Instance.ShowNotification(
            "Offline Progress",
            $"You earned {totalGold} gold while offline!",
            NotificationSystem.NotificationType.Success
        );

        Debug.Log($"Offline gains applied for {offlineHours} hours");
    }
}
