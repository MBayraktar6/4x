using System.Collections.Generic;
using UnityEngine;

public class DivineFavorSystem
{
    private Dictionary<string, float> favorRatings; // God name -> favor value (0-100)
    private Dictionary<string, List<string>> blessings; // God name -> active blessings

    public DivineFavorSystem()
    {
        favorRatings = new Dictionary<string, float>();
        blessings = new Dictionary<string, List<string>>();

        // Initialize gods
        string[] gods = { "Zeus", "Ares", "Aphrodite", "Athena", "Poseidon", "Hades" };
        foreach (string god in gods)
        {
            favorRatings[god] = 50f; // Neutral favor
            blessings[god] = new List<string>();
        }
    }

    public void IncreaseFavor(string god, float amount)
    {
        if (favorRatings.ContainsKey(god))
        {
            favorRatings[god] = Mathf.Min(100f, favorRatings[god] + amount);
            CheckForBlessing(god);
        }
    }

    public void DecreaseFavor(string god, float amount)
    {
        if (favorRatings.ContainsKey(god))
        {
            favorRatings[god] = Mathf.Max(0f, favorRatings[god] - amount);
            CheckForCurse(god);
        }
    }

    public float GetFavor(string god)
    {
        return favorRatings.ContainsKey(god) ? favorRatings[god] : 50f;
    }

    private void CheckForBlessing(string god)
    {
        if (favorRatings[god] > 75f && !blessings[god].Contains("Divine Strength"))
        {
            blessings[god].Add("Divine Strength");
        }
        if (favorRatings[god] > 85f && !blessings[god].Contains("Immortal Vigor"))
        {
            blessings[god].Add("Immortal Vigor");
        }
    }

    private void CheckForCurse(string god)
    {
        if (favorRatings[god] < 25f)
        {
            blessings[god].Clear();
            blessings[god].Add("Divine Wrath");
        }
    }

    public List<string> GetActiveBlessings(string god)
    {
        return blessings.ContainsKey(god) ? blessings[god] : new List<string>();
    }
}
