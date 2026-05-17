using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ClanMenuUI : MonoBehaviour
{
    [Header("Clan Info Panel")]
    public TextMeshProUGUI clanNameText;
    public TextMeshProUGUI clanLevelText;
    public TextMeshProUGUI clanMembersCountText;
    public TextMeshProUGUI clanGoldText;
    public Slider clanExpSlider;

    [Header("Members List")]
    public Transform memberListParent;
    public GameObject memberListItemPrefab;
    public ScrollRect memberScrollRect;

    [Header("Territory Info")]
    public TextMeshProUGUI territoryCountText;
    public TextMeshProUGUI clanRankText;

    [Header("Buttons")]
    public Button createClanButton;
    public Button joinClanButton;
    public Button leaveClanButton;
    public Button clanSettingsButton;
    public Button closeClanMenuButton;

    public GameObject clanMenuPanel;
    public GameObject createClanDialog;
    public InputField clanNameInput;

    private void Start()
    {
        createClanButton.onClick.AddListener(OpenCreateClanDialog);
        joinClanButton.onClick.AddListener(OpenJoinClanDialog);
        leaveClanButton.onClick.AddListener(LeaveClan);
        closeClanMenuButton.onClick.AddListener(CloseClanMenu);
    }

    public void OpenClanMenu()
    {
        clanMenuPanel.SetActive(true);
        RefreshClanInfo();
    }

    public void CloseClanMenu()
    {
        clanMenuPanel.SetActive(false);
    }

    private void RefreshClanInfo()
    {
        ClanManager clanManager = GameManager.Instance.clanManager;
        if (clanManager.playerClan != null)
        {
            ClanManager.Clan clan = clanManager.playerClan;
            clanNameText.text = clan.clanName;
            clanLevelText.text = "Level " + clan.level;
            clanMembersCountText.text = "Members: " + clan.memberIds.Count;
            clanGoldText.text = "Gold: " + clan.clanGold;
            territoryCountText.text = "Territory: " + clan.ownedTerritories.Count + " tiles";

            long requiredExp = clan.level * 10000;
            clanExpSlider.value = (float)clan.experience / requiredExp;

            RefreshMembersList(clan);
        }
        else
        {
            clanNameText.text = "No Clan";
            clanLevelText.text = "Level 0";
        }
    }

    private void RefreshMembersList(ClanManager.Clan clan)
    {
        // Clear existing items
        foreach (Transform child in memberListParent)
        {
            Destroy(child.gameObject);
        }

        // Populate with new members
        foreach (int memberId in clan.memberIds)
        {
            GameObject memberItem = Instantiate(memberListItemPrefab, memberListParent);
            TextMeshProUGUI memberText = memberItem.GetComponent<TextMeshProUGUI>();
            if (memberText != null)
            {
                memberText.text = "Member ID: " + memberId;
            }
        }
    }

    public void OpenCreateClanDialog()
    {
        createClanDialog.SetActive(true);
    }

    public void CreateClan()
    {
        string clanName = clanNameInput.text;
        if (string.IsNullOrEmpty(clanName))
        {
            Debug.LogWarning("Clan name cannot be empty");
            return;
        }

        ClanManager clanManager = GameManager.Instance.clanManager;
        clanManager.CreateClan(clanName, 0); // 0 is player ID
        createClanDialog.SetActive(false);
        RefreshClanInfo();
    }

    public void OpenJoinClanDialog()
    {
        Debug.Log("Open join clan dialog");
        // Implement join clan dialog
    }

    public void LeaveClan()
    {
        ClanManager clanManager = GameManager.Instance.clanManager;
        PlayerData playerData = GameManager.Instance.playerData;
        if (clanManager.playerClan != null)
        {
            clanManager.LeaveClan(0, clanManager.playerClan.clanId);
            playerData.playerInfo.clanId = -1;
            RefreshClanInfo();
        }
    }
}
