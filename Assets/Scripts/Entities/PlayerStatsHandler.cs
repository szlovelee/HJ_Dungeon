using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] PlayerStats baseStats;

    public PlayerStats CurrentStats { get; private set; }
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
        UpdateCharacterStats();
    }

    public void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStats = new PlayerStats { attackSO = attackSO };

        UpdateStats((a, b) => b, baseStats);    //(a, b) ==> b�� �� �����͸� �޾ƿͼ� ���ڸ� ���ڴٴ� ��


        foreach (Items item in gameManager.inventory.EquippedItems)
        {
            UpdateAttackStats((o, o1) => o + o1, CurrentStats.attackSO, item.attackSO);
        }

    }

    private void UpdateStats(Func<float, float, float> operation, PlayerStats modifier)
    {
        CurrentStats.level = (int)operation(CurrentStats.level, modifier.level);
        CurrentStats.exp = (int)operation(CurrentStats.exp, modifier.exp);
        CurrentStats.coin = (int)operation(CurrentStats.coin, modifier.coin);

        UpdateAttackStats(operation, CurrentStats.attackSO, modifier.attackSO);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        if (currentAttack.GetType() != newAttack.GetType())
        {
            return;
        }

        currentAttack.attack = (int)operation(currentAttack.attack, newAttack.attack);
        currentAttack.defense = (int)operation(currentAttack.defense, newAttack.defense);
        currentAttack.critical = (int)operation(currentAttack.critical, newAttack.critical);
    }
}
