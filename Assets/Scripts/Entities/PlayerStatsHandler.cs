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

        CurrentStats.level = 1;
        CurrentStats.exp = 1;
        CurrentStats.coin = 999999;
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

        AddItemStats(CurrentStats.attackSO, Equipped);
    }

    private void UpdateStats(Func<string, string, string> operation, PlayerStats modifier)
    {
        CurrentStats.id = (string)operation(CurrentStats.id, modifier.id);
        CurrentStats.desc = (string)operation(CurrentStats.desc, modifier.desc);
        CurrentStats.level = int.Parse(operation(CurrentStats.level.ToString(), modifier.level.ToString()));
        CurrentStats.exp = int.Parse(operation(CurrentStats.exp.ToString(), modifier.exp.ToString().ToString()));
        CurrentStats.coin = int.Parse(operation(CurrentStats.coin.ToString(), modifier.coin.ToString().ToString()));

        CurrentStats.attackSO.baseAttack = modifier.attackSO.baseAttack;
        CurrentStats.attackSO.baseDefense = modifier.attackSO.baseDefense;
        CurrentStats.attackSO.baseMaxHealth = modifier.attackSO.baseMaxHealth;
        CurrentStats.attackSO.baseCritical = modifier.attackSO.baseCritical;

    }

    private void AddItemStats(AttackSO currentAttack, Items[] Eqipped)
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
