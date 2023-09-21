﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] PlayerStats baseStats;

    public PlayerStats CurrentStats { get; private set; }
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Start()
    {
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

        UpdateStats((a, b) => b, baseStats);    //(a, b) ==> b는 두 데이터를 받아와서 후자를 쓰겠다는 뜻

        if (!gameManager.inventory.EquippedItems.All(item => item == null))     // EquippedItems의 요소가 모두 null인 경우 예외처리
        {
            foreach (Items item in gameManager.inventory.EquippedItems)
            {
                UpdateAttackStats((o, o1) => o + o1, CurrentStats.attackSO, item);
            }
        }
    }

    private void UpdateStats(Func<float, float, float> operation, PlayerStats modifier)
    {
        CurrentStats.level = (int)operation(CurrentStats.level, modifier.level);
        CurrentStats.exp = (int)operation(CurrentStats.exp, modifier.exp);
        CurrentStats.coin = (int)operation(CurrentStats.coin, modifier.coin);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, Items item)
    {
        if (currentAttack == null || item == null)
        {
            return;
        }

        if (currentAttack.GetType() != item.GetType())
        {
            return;
        }

        currentAttack.attack = (int)operation(currentAttack.attack, item.attack);
        currentAttack.defense = (int)operation(currentAttack.defense, item.defense);
        currentAttack.critical = (int)operation(currentAttack.critical, item.critical);
    }
}
