public class GameState
{
    public int WolfPopulation { get; set; }
    public int PreyPopulation { get; set; }
    public int CurrentTurn { get; set; }
    public int Year { get; set; }
    public float Temperature { get; set; }

    public GameState()
    {
        WolfPopulation = 10;
        PreyPopulation = 50;
        CurrentTurn = 0;
        Year = 1;
        Temperature = 20f;
    }

    public void UpdateTurn()
    {
        CurrentTurn++;
        if (CurrentTurn % 4 == 0)
            Year++;
    }
}
