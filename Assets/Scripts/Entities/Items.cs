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


[CreateAssetMenu(fileName = "ItemData", menuName = "HJ_Dungeon/ItemData", order = 0)]
public class Items : ScriptableObject
{
    public string itemName;
    public ItemType Type;
    public int attack;
    public int defense;
    public int maxHealth;
    public int critical;
    public Sprite itemIcon;
}
