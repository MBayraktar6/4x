using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle soundToggle;

    [Header("Graphics Settings")]
    public Toggle lowGraphicsToggle;
    public Toggle mediumGraphicsToggle;
    public Toggle highGraphicsToggle;
    public Slider fpsLimitSlider;

    [Header("Gameplay Settings")]
    public Toggle pushNotificationsToggle;
    public Toggle offlineProgressToggle;
    public Toggle autoSaveToggle;
    public Slider gameSpeedSlider;

    [Header("Account Settings")]
    public TextMeshProUGUI playerIdText;
    public TextMeshProUGUI accountCreatedText;
    public Button changeUsernameButton;
    public Button resetGameButton;
    public Button logoutButton;

    [Header("UI")]
    public GameObject settingsPanel;
    public Button closeSettingsButton;

    private void Start()
    {
        InitializeSettings();
        closeSettingsButton.onClick.AddListener(CloseSettings);
        resetGameButton.onClick.AddListener(ResetGame);
    }

    private void InitializeSettings()
    {
        // Load settings from PlayerPrefs or config
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        soundToggle.onValueChanged.AddListener(OnSoundToggled);
        pushNotificationsToggle.onValueChanged.AddListener(OnNotificationsToggled);
        autoSaveToggle.onValueChanged.AddListener(OnAutoSaveToggled);

        gameSpeedSlider.onValueChanged.AddListener(OnGameSpeedChanged);
        fpsLimitSlider.onValueChanged.AddListener(OnFPSLimitChanged);

        // Display player info
        PlayerData playerData = GameManager.Instance.playerData;
        playerIdText.text = "Player ID: " + playerData.playerInfo.playerName;
        accountCreatedText.text = "Account created";
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    private void OnMasterVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void OnSoundToggled(bool isEnabled)
    {
        PlayerPrefs.SetInt("SoundEnabled", isEnabled ? 1 : 0);
    }

    private void OnNotificationsToggled(bool isEnabled)
    {
        PlayerPrefs.SetInt("NotificationsEnabled", isEnabled ? 1 : 0);
    }

    private void OnAutoSaveToggled(bool isEnabled)
    {
        PlayerPrefs.SetInt("AutoSaveEnabled", isEnabled ? 1 : 0);
    }

    private void OnGameSpeedChanged(float value)
    {
        GameManager.Instance.gameSpeed = value;
    }

    private void OnFPSLimitChanged(float value)
    {
        Application.targetFrameRate = (int)value;
        PlayerPrefs.SetInt("FPSLimit", (int)value);
    }

    public void ResetGame()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Are you sure you want to reset? This cannot be undone!");
            PlayerPrefs.DeleteAll();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
