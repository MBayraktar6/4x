using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState currentState;
    private int currentTurn = 0;

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
        Initialize();
    }

    public void Initialize()
    {
        currentState = new GameState();
        currentTurn = 0;
    }

    public void EndTurn()
    {
        currentTurn++;
    }

    public int GetCurrentTurn() => currentTurn;
    public GameState GetGameState() => currentState;
}
