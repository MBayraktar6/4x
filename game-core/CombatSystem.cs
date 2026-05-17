using UnityEngine;

public class CombatSystem
{
    public static float CalculateDamage(Unit attacker, Unit defender)
    {
        float baseDamage = 20f;
        float attackerBonus = attacker.Health / attacker.MaxHealth;
        float defenderReduction = defender.Health / defender.MaxHealth;

        float damage = baseDamage * attackerBonus * (1f - defenderReduction * 0.5f);
        return Mathf.Max(5f, damage);
    }

    public static bool Combat(Unit attacker, Unit defender)
    {
        if (!attacker.IsAlive || !defender.IsAlive)
            return false;

        float damage = CalculateDamage(attacker, defender);
        defender.TakeDamage(damage);

        if (!defender.IsAlive)
            return true;

        // Defender counterattack
        float counterDamage = CalculateDamage(defender, attacker);
        attacker.TakeDamage(counterDamage);

        return false;
    }

    public static bool CanEngage(Unit unit1, Unit unit2)
    {
        return Vector2.Distance(new Vector2(unit1.X, unit1.Y), new Vector2(unit2.X, unit2.Y)) <= 1.5f;
    }
}
