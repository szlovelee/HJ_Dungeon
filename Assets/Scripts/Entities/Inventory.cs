﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static GameManager gameManager = GameManager.instance;
    private PlayerStatsHandler statsHandler = gameManager.player.GetComponent<PlayerStatsHandler>();

    public List<Items> InventoryItems = new List<Items>();

    public Items[] EquippedItems = new Items[4];

    public void EquipItem(Items item)
    {
        if (EquippedItems[(int)item.Type] != null)
        {
            //메시지 띄우기
        }
        EquippedItems[(int)item.Type] = item;
        statsHandler.UpdateCharacterStats();
    }

    public void RemoveItem(Items item)
    {
        EquippedItems[(int)item.Type] = null;
        statsHandler.UpdateCharacterStats();
    }
}
