using UnityEngine;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    [System.Serializable]
    public class Achievement
    {
        public string achievementId;
        public string title;
        public string description;
        public long rewardGold;
        public int rewardPoints;
        public bool isCompleted;
        public int progress;
        public int requiredProgress;
    }

    public List<Achievement> achievements = new List<Achievement>();
    private Dictionary<string, Achievement> achievementDict = new Dictionary<string, Achievement>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        // Initialize all available achievements
        achievements.Add(new Achievement
        {
            achievementId = "first_building",
            title = "Builder",
            description = "Build your first building",
            rewardGold = 500,
            rewardPoints = 10,
            requiredProgress = 1
        });

        achievements.Add(new Achievement
        {
            achievementId = "train_100_units",
            title = "Commander",
            description = "Train 100 units",
            rewardGold = 2000,
            rewardPoints = 50,
            requiredProgress = 100
        });

        achievements.Add(new Achievement
        {
            achievementId = "reach_level_10",
            title = "Veteran",
            description = "Reach player level 10",
            rewardGold = 1000,
            rewardPoints = 25,
            requiredProgress = 10
        });

        achievements.Add(new Achievement
        {
            achievementId = "win_10_battles",
            title = "Warrior",
            description = "Win 10 battles",
            rewardGold = 1500,
            rewardPoints = 40,
            requiredProgress = 10
        });

        achievements.Add(new Achievement
        {
            achievementId = "watch_50_ads",
            title = "Ad Watcher",
            description = "Watch 50 reward ads",
            rewardGold = 3000,
            rewardPoints = 30,
            requiredProgress = 50
        });

        foreach (Achievement ach in achievements)
        {
            achievementDict[ach.achievementId] = ach;
        }
    }

    public void UpdateAchievementProgress(string achievementId, int amount = 1)
    {
        if (achievementDict.ContainsKey(achievementId))
        {
            Achievement ach = achievementDict[achievementId];
            if (!ach.isCompleted)
            {
                ach.progress += amount;
                if (ach.progress >= ach.requiredProgress)
                {
                    CompleteAchievement(achievementId);
                }
            }
        }
    }

    private void CompleteAchievement(string achievementId)
    {
        if (achievementDict.ContainsKey(achievementId))
        {
            Achievement ach = achievementDict[achievementId];
            ach.isCompleted = true;

            // Award player
            PlayerData playerData = GameManager.Instance.playerData;
            playerData.AddResources("gold", ach.rewardGold);
            playerData.AddExperience(ach.rewardPoints);

            // Show notification
            NotificationSystem.Instance.ShowNotification(
                "Achievement Unlocked!",
                ach.title + ": " + ach.description,
                NotificationSystem.NotificationType.Success
            );

            Debug.Log("Achievement completed: " + ach.title);
        }
    }

    public List<Achievement> GetAllAchievements()
    {
        return achievements;
    }
}
