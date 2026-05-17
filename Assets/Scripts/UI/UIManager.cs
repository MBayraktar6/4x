using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Resource Display")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI ironText;

    [Header("Player Info")]
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerLevelText;
    public Slider playerExpSlider;

    [Header("Building UI")]
    public GameObject buildingPanelPrefab;
    public Transform buildingPanelParent;

    [Header("Buttons")]
    public Button buildButton;
    public Button trainButton;
    public Button attackButton;
    public Button clanButton;
    public Button settingsButton;
    public Button rewardButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        InitializeUI();
        RegisterButtonCallbacks();
    }

    private void InitializeUI()
    {
        UpdateResourceDisplay();
        UpdatePlayerInfo();
    }

    private void RegisterButtonCallbacks()
    {
        buildButton.onClick.AddListener(OpenBuildMenu);
        trainButton.onClick.AddListener(OpenTrainMenu);
        attackButton.onClick.AddListener(OpenAttackMenu);
        clanButton.onClick.AddListener(OpenClanMenu);
        settingsButton.onClick.AddListener(OpenSettings);
        rewardButton.onClick.AddListener(OnRewardButtonClicked);
    }

    private void Update()
    {
        UpdateResourceDisplay();
    }

    private void UpdateResourceDisplay()
    {
        PlayerData playerData = GameManager.Instance.playerData;
        goldText.text = FormatNumber(playerData.resources.gold);
        woodText.text = FormatNumber(playerData.resources.wood);
        stoneText.text = FormatNumber(playerData.resources.stone);
        foodText.text = FormatNumber(playerData.resources.food);
        ironText.text = FormatNumber(playerData.resources.iron);
    }

    private void UpdatePlayerInfo()
    {
        PlayerData playerData = GameManager.Instance.playerData;
        playerNameText.text = playerData.playerInfo.playerName;
        playerLevelText.text = "Level " + playerData.playerInfo.level;

        long requiredExp = playerData.playerInfo.level * 1000;
        playerExpSlider.value = (float)playerData.playerInfo.experience / requiredExp;
    }

    private string FormatNumber(long number)
    {
        if (number >= 1000000)
            return (number / 1000000f).ToString("F1") + "M";
        else if (number >= 1000)
            return (number / 1000f).ToString("F1") + "K";
        else
            return number.ToString();
    }

    public void OpenBuildMenu()
    {
        Debug.Log("Build menu opened");
        // Implement build menu UI
    }

    public void OpenTrainMenu()
    {
        Debug.Log("Train menu opened");
        // Implement train menu UI
    }

    public void OpenAttackMenu()
    {
        Debug.Log("Attack menu opened");
        // Implement attack menu UI
    }

    public void OpenClanMenu()
    {
        Debug.Log("Clan menu opened");
        // Implement clan menu UI
    }

    public void OpenSettings()
    {
        Debug.Log("Settings opened");
        // Implement settings UI
    }

    private void OnRewardButtonClicked()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

    public void ShowNotification(string message)
    {
        Debug.Log("Notification: " + message);
        // Implement notification UI
    }
}
