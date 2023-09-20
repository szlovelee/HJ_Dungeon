using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Shield,
    Armor,
    Ring
}

public class Items : MonoBehaviour
{
    public ItemType Type;
    public AttackSO attackSO;
}
