using UnityEngine;
using System.Collections.Generic;

public class CombatSystem : MonoBehaviour
{
    [System.Serializable]
    public class Battle
    {
        public int battleId;
        public int attackerClanId;
        public int defenderClanId;
        public Vector3 battleLocation;
        public List<Unit> attackerUnits = new List<Unit>();
        public List<Unit> defenderUnits = new List<Unit>();
        public bool isActive = false;
        public float battleDuration = 0f;
    }

    public List<Battle> activeBattles = new List<Battle>();
    private int nextBattleId = 1;

    public Battle StartBattle(int attackerClanId, int defenderClanId, Vector3 location, 
                              List<Unit> attackerUnits, List<Unit> defenderUnits)
    {
        Battle battle = new Battle
        {
            battleId = nextBattleId++,
            attackerClanId = attackerClanId,
            defenderClanId = defenderClanId,
            battleLocation = location,
            attackerUnits = new List<Unit>(attackerUnits),
            defenderUnits = new List<Unit>(defenderUnits),
            isActive = true
        };

        activeBattles.Add(battle);
        Debug.Log("Battle started: Clan " + attackerClanId + " vs Clan " + defenderClanId);
        return battle;
    }

    private void Update()
    {
        for (int i = activeBattles.Count - 1; i >= 0; i--)
        {
            if (activeBattles[i].isActive)
            {
                UpdateBattle(activeBattles[i]);
            }
            else
            {
                activeBattles.RemoveAt(i);
            }
        }
    }

    private void UpdateBattle(Battle battle)
    {
        battle.battleDuration += Time.deltaTime;

        // Remove defeated units
        battle.attackerUnits.RemoveAll(u => u == null || u.unitData.health <= 0);
        battle.defenderUnits.RemoveAll(u => u == null || u.unitData.health <= 0);

        // Check if battle is over
        if (battle.attackerUnits.Count == 0 || battle.defenderUnits.Count == 0)
        {
            EndBattle(battle);
        }
        else
        {
            // Combat logic
            PerformCombatRound(battle);
        }
    }

    private void PerformCombatRound(Battle battle)
    {
        // Simple combat: each unit attacks a random enemy
        foreach (Unit attacker in battle.attackerUnits)
        {
            if (battle.defenderUnits.Count > 0)
            {
                Unit defender = battle.defenderUnits[Random.Range(0, battle.defenderUnits.Count)];
                int damage = attacker.GetAttackDamage();
                defender.TakeDamage(damage);
            }
        }

        foreach (Unit attacker in battle.defenderUnits)
        {
            if (battle.attackerUnits.Count > 0)
            {
                Unit defender = battle.attackerUnits[Random.Range(0, battle.attackerUnits.Count)];
                int damage = attacker.GetAttackDamage();
                defender.TakeDamage(damage);
            }
        }
    }

    private void EndBattle(Battle battle)
    {
        battle.isActive = false;

        if (battle.attackerUnits.Count > 0)
        {
            Debug.Log("Battle won by clan " + battle.attackerClanId);
            GiveBattleRewards(battle.attackerClanId, 1000);
        }
        else
        {
            Debug.Log("Battle won by clan " + battle.defenderClanId);
            GiveBattleRewards(battle.defenderClanId, 500);
        }
    }

    private void GiveBattleRewards(int clanId, long amount)
    {
        GameManager.Instance.clanManager.AddClanExperience(clanId, amount);
    }
}
