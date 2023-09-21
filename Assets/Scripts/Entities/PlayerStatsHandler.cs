using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] PlayerStats baseStats;

    public PlayerStats CurrentStats { get; private set; }
    private GameManager gameManager;
    private Items[] Equipped;

    private void Awake()
    {
        gameManager = GameManager.instance;
        Equipped = gameManager.inventory.EquippedItems;
        UpdateCharacterStats();
    }

    public void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStats = ScriptableObject.CreateInstance<PlayerStats>();
        CurrentStats.attackSO = attackSO;

        UpdateStats((a, b) => b, baseStats);

        UpdateAttackStats(CurrentStats.attackSO, Equipped);
    }

    private void UpdateStats(Func<float, float, float> operation, PlayerStats modifier)
    {
        CurrentStats.level = (int)operation(CurrentStats.level, modifier.level);
        CurrentStats.exp = (int)operation(CurrentStats.exp, modifier.exp);
        CurrentStats.coin = (int)operation(CurrentStats.coin, modifier.coin);
    }

    private void UpdateAttackStats(AttackSO currentAttack, Items[] Eqipped)
    {
        CurrentStats.attack = currentAttack.baseAttack;
        CurrentStats.defense = currentAttack.baseDefense;
        CurrentStats.maxHealth = currentAttack.baseMaxHealth;
        CurrentStats.critical = currentAttack.baseCritical;


        foreach (Items item in Equipped)
        {
            if (item == null) continue;
            CurrentStats.attack += item.attack;
            CurrentStats.defense += item.defense;
            CurrentStats.maxHealth += item.maxHealth;
            CurrentStats.critical += item.critical;
        }
    }
}
