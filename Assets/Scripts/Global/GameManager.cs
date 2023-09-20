using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public Inventory inventory;

    private void Awake()
    {
        instance = this;
        inventory = new Inventory();
    }
}
