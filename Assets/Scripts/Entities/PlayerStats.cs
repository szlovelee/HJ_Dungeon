using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "HJ_Dungeon/PlayerStats", order = 0)]

public class PlayerStats : ScriptableObject
{
    public string id;
    public string desc;

    private int _level;
    public int level
    {
        get { return _level; }
        set
        {
            _level = value;
            maxExp = 10 + (_level * 2);
        }
    }

    private int _maxExp;
    public int maxExp
    {
        get { return _maxExp; }
        private set
        {
            _maxExp = value;
        }
    }
    public int attack;
    public int defense;
    public int maxHealth;
    public int critical;

    public int exp;
    public int coin;

    public AttackSO attackSO;
}
