using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "HJ_Dungeon/Attacks/Default", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public int baseAttack;
    public int baseDefense;
    public int baseMaxHealth;
    public int baseCritical;
}
