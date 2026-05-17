using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Kaynak Göstergeleri")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI gemsText;

    [Header("Reklam UI")]
    [SerializeField] private Button watchAdButton;
    [SerializeField] private TextMeshProUGUI adCooldownText;
    [SerializeField] private TextMeshProUGUI adLimitText;

    private PlayerData playerData;
    private AdManager adManager;
    private GameConfig config;
    private float adUIUpdateTimer = 0f;

    public void Initialize()
    {
        playerData = GameManager.Instance.GetPlayerData();
        adManager = GameManager.Instance.GetAdManager();
        config = GetComponent<GameManager>()?.GetComponent<GameConfig>();

        if (watchAdButton != null)
            watchAdButton.onClick.AddListener(OnWatchAdButtonClicked);

        if (adManager != null)
            adManager.OnVideoRewarded += OnVideoRewarded;

        UpdateUI();
        Debug.Log("[UIManager] UI başlatıldı.");
    }

    private void Update()
    {
        UpdateResourceDisplay();
        UpdateAdUI();
    }

    private void UpdateResourceDisplay()
    {
        if (playerData == null) return;

        if (goldText != null)
            goldText.text = FormatNumber(playerData.resources.gold);
        if (woodText != null)
            woodText.text = FormatNumber(playerData.resources.wood);
        if (foodText != null)
            foodText.text = FormatNumber(playerData.resources.food);
        if (gemsText != null)
            gemsText.text = FormatNumber(playerData.resources.gems);
    }

    private void UpdateAdUI()
    {
        if (adManager == null || adCooldownText == null) return;

        adUIUpdateTimer += Time.deltaTime;
        if (adUIUpdateTimer < 0.5f) return; // Her 0.5 saniyede güncelle
        adUIUpdateTimer = 0f;

        long currentTime = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long lastAdTime = playerData.adStats.lastAdWatchTime;
        long cooldownSeconds = config.videoAdCooldownMinutes * 60;
        long remainingCooldown = Mathf.Max(0, cooldownSeconds - (currentTime - lastAdTime));

        if (remainingCooldown > 0)
        {
            int minutes = (int)(remainingCooldown / 60);
            int seconds = (int)(remainingCooldown % 60);
            adCooldownText.text = $"Hazır olacak: {minutes:D2}:{seconds:D2}";
            watchAdButton.interactable = false;
        }
        else
        {
            adCooldownText.text = "Reklam hazır!";
            watchAdButton.interactable = true;
        }

        if (adLimitText != null)
        {
            adLimitText.text = $"Günlük: {playerData.adStats.adWatchCountToday}/{config.dailyVideoAdLimit}";
        }
    }

    private void OnWatchAdButtonClicked()
    {
        Debug.Log("[UIManager] Reklam izle düğmesine basıldı.");
        adManager.ShowRewardedAd();
    }

    private void OnVideoRewarded(int gold, int wood, int food)
    {
        Debug.Log($"[UIManager] Ödül gösterildi: {gold}G, {wood}W, {food}F");
        // Ödül animasyonu veya bildirim gösterilebilir
    }

    public void UpdateUI()
    {
        UpdateResourceDisplay();
        UpdateAdUI();
    }

    private string FormatNumber(long number)
    {
        if (number >= 1000000)
            return (number / 1000000f).ToString("F1") + "M";
        if (number >= 1000)
            return (number / 1000f).ToString("F1") + "K";
        return number.ToString();
    }
}
