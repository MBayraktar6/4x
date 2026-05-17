using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialSystem : MonoBehaviour
{
    public static TutorialSystem Instance { get; private set; }

    [System.Serializable]
    public class TutorialStep
    {
        public string stepId;
        public string title;
        public string description;
        public GameObject targetUIElement;
        public bool isCompleted;
    }

    [Header("Tutorial UI")]
    public GameObject tutorialPanel;
    public TextMeshProUGUI stepTitleText;
    public TextMeshProUGUI stepDescriptionText;
    public Button nextStepButton;
    public Button skipTutorialButton;
    public Image targetHighlight;

    private List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    private int currentStepIndex = 0;
    private bool tutorialActive = true;

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
        InitializeTutorialSteps();
        nextStepButton.onClick.AddListener(NextStep);
        skipTutorialButton.onClick.AddListener(SkipTutorial);

        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 0)
        {
            StartTutorial();
        }
    }

    private void InitializeTutorialSteps()
    {
        tutorialSteps.Add(new TutorialStep
        {
            stepId = "welcome",
            title = "Welcome",
            description = "Welcome to the 4X Strategy Game! Your goal is to build your village, train units, and compete with other clans."
        });

        tutorialSteps.Add(new TutorialStep
        {
            stepId = "resources",
            title = "Resources",
            description = "You have 5 types of resources: Gold, Wood, Stone, Food, and Iron. Manage them wisely!"
        });

        tutorialSteps.Add(new TutorialStep
        {
            stepId = "buildings",
            title = "Buildings",
            description = "Buildings produce resources over time. Build farms for food, lumber mills for wood, etc."
        });

        tutorialSteps.Add(new TutorialStep
        {
            stepId = "units",
            title = "Units",
            description = "Train units in your barracks to defend your village or attack enemies."
        });

        tutorialSteps.Add(new TutorialStep
        {
            stepId = "clans",
            title = "Clans",
            description = "Join or create a clan to cooperate with other players and expand your territory."
        });
    }

    public void StartTutorial()
    {
        tutorialActive = true;
        currentStepIndex = 0;
        ShowCurrentStep();
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex < tutorialSteps.Count)
        {
            TutorialStep step = tutorialSteps[currentStepIndex];
            stepTitleText.text = step.title;
            stepDescriptionText.text = step.description;
            tutorialPanel.SetActive(true);
        }
    }

    public void NextStep()
    {
        currentStepIndex++;
        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowCurrentStep();
        }
        else
        {
            CompleteTutorial();
        }
    }

    public void SkipTutorial()
    {
        CompleteTutorial();
    }

    private void CompleteTutorial()
    {
        tutorialPanel.SetActive(false);
        tutorialActive = false;
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        Debug.Log("Tutorial completed");
    }
}
