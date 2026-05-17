using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    [SerializeField] private string rewardedAdUnitId = "ca-app-pub-xxxxxxxxxxxxxxxx/yyyyyyyyyyyyyy"; // Google AdMob ID
    private RewardedAd rewardedAd;
    private bool adReady = false;

    public event Action<int, int, int> OnVideoRewarded; // gold, wood, food

    private GameConfig config;
    private PlayerData playerData;

    public void Initialize()
    {
        MobileAds.Initialize();
        LoadRewardedAd();
        Debug.Log("[AdManager] Google Mobile Ads başlatıldı.");
    }

    public void Initialize(GameConfig gameConfig, PlayerData data)
    {
        config = gameConfig;
        playerData = data;
        Initialize();
    }

    private void LoadRewardedAd()
    {
        AdRequest request = new AdRequest();
        RewardedAd.Load(rewardedAdUnitId, request, HandleRewardedAdLoaded);
    }

    private void HandleRewardedAdLoaded(RewardedAd ad, LoadAdError error)
    {
        if (error != null || ad == null)
        {
            Debug.LogError("[AdManager] Reklam yükleme başarısız: " + error?.GetMessage());
            adReady = false;
            return;
        }

        rewardedAd = ad;
        adReady = true;
        RegisterEventHandlers(rewardedAd);
        Debug.Log("[AdManager] Ödüllü reklam yüklendi.");
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += HandleAdClosed;
        ad.OnAdFullScreenContentFailed += HandleAdFailedToOpen;
        ad.OnAdPaid += HandleAdPaid;
    }

    public bool CanWatchAd()
    {
        if (!adReady)
        {
            Debug.Log("[AdManager] Reklam henüz yüklenmedi.");
            return false;
        }

        // Cooldown kontrolü
        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long lastAdTime = playerData.adStats.lastAdWatchTime;
        long cooldownSeconds = config.videoAdCooldownMinutes * 60;

        if (currentTime - lastAdTime < cooldownSeconds)
        {
            Debug.LogWarning($"[AdManager] Reklam cooldown'unda. Kalan süre: {(cooldownSeconds - (currentTime - lastAdTime))} saniye");
            return false;
        }

        // Günlük limit kontrolü
        if (IsNewDay())
        {
            playerData.adStats.adWatchCountToday = 0;
            playerData.adStats.dailyAdResetTime = currentTime + (24 * 3600);
        }

        if (playerData.adStats.adWatchCountToday >= config.dailyVideoAdLimit)
        {
            Debug.LogWarning($"[AdManager] Günlük reklam limitine ulaştınız: {config.dailyVideoAdLimit}");
            return false;
        }

        return true;
    }

    public void ShowRewardedAd()
    {
        if (!CanWatchAd())
        {
            Debug.LogWarning("[AdManager] Şu anda reklam izleyemezsiniz.");
            return;
        }

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                HandleRewardUser();
            });
        }
        else
        {
            Debug.LogError("[AdManager] Ödüllü reklam gösterilemeyen başarısız.");
        }
    }

    private void HandleRewardUser()
    {
        Debug.Log("[AdManager] Kullanıcı reklam izledi ve ödül aldı.");

        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        playerData.adStats.lastAdWatchTime = currentTime;
        playerData.adStats.adWatchCountToday++;

        // Ödülleri ver
        playerData.AddResource(ResourceType.Gold, config.goldRewardPerVideo);
        playerData.AddResource(ResourceType.Wood, config.woodRewardPerVideo);
        playerData.AddResource(ResourceType.Food, config.foodRewardPerVideo);

        OnVideoRewarded?.Invoke(
            (int)config.goldRewardPerVideo,
            (int)config.woodRewardPerVideo,
            (int)config.foodRewardPerVideo
        );

        // Yeni reklam yükle
        LoadRewardedAd();
    }

    private bool IsNewDay()
    {
        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return currentTime >= playerData.adStats.dailyAdResetTime;
    }

    private void HandleAdClosed()
    {
        Debug.Log("[AdManager] Reklam kapatıldı.");
        LoadRewardedAd();
    }

    private void HandleAdFailedToOpen(AdError error)
    {
        Debug.LogError("[AdManager] Reklam açılamadı: " + error);
        LoadRewardedAd();
    }

    private void HandleAdPaid(AdValue value)
    {
        Debug.Log($"[AdManager] Reklam ödeme alındı: {value.Value}");
    }

    private void Update()
    {
        // Periyodik kontrol
    }
}
