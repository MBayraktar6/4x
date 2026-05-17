using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }

    [Header("AdMob Settings")]
    public string appId = "ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"; // Replace with your app ID
    public string rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test ID
    public string bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ID

    [Header("Reward Settings")]
    public long rewardAmount = 100;
    public int cooldownMinutes = 15;
    private float lastRewardTime = 0f;
    private bool canWatchAd = true;

    private RewardedAd rewardedAd;
    private BannerView bannerView;

    public delegate void OnRewardCallback(long amount);
    public event OnRewardCallback OnRewardEarned;

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
        InitializeMobileAds();
        LoadRewardedAd();
        LoadBannerAd();
    }

    private void InitializeMobileAds()
    {
        MobileAds.Initialize(initStatus => { });
        Debug.Log("Mobile Ads initialized");
    }

    private void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }

        var adRequest = new AdRequest();
        RewardedAd.Load(rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load");
                    return;
                }

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }

    private void LoadBannerAd()
    {
        var adRequest = new AdRequest();
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Top);
        bannerView.LoadAd(adRequest);
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded Ad closed");
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded Ad failed: " + error);
            LoadRewardedAd();
        };

        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Rewarded Ad paid");
        };
    }

    public void ShowRewardedAd()
    {
        if (!CanWatchAd())
        {
            float timeRemaining = cooldownMinutes * 60f - (Time.time - lastRewardTime);
            Debug.Log("Ad cooldown active. Time remaining: " + Mathf.Ceil(timeRemaining) + " seconds");
            return;
        }

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                EarnReward();
            });
        }
        else
        {
            Debug.LogWarning("Rewarded ad is not ready yet");
        }
    }

    private void EarnReward()
    {
        lastRewardTime = Time.time;
        PlayerData playerData = GameManager.Instance.playerData;
        playerData.AddResources("gold", rewardAmount);
        OnRewardEarned?.Invoke(rewardAmount);
        Debug.Log("Reward earned: " + rewardAmount + " gold");
    }

    public bool CanWatchAd()
    {
        return (Time.time - lastRewardTime) >= (cooldownMinutes * 60f);
    }

    public float GetAdCooldownPercent()
    {
        float elapsed = Time.time - lastRewardTime;
        float totalCooldown = cooldownMinutes * 60f;
        return (elapsed / totalCooldown) * 100f;
    }

    private void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}
