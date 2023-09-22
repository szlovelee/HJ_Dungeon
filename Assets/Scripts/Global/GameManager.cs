using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public Items[] ItemList;
    public Inventory inventory;

    public event Action OnStatusChange;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        inventory = new Inventory();
        ItemList = Resources.LoadAll<Items>("Items");
    }

    private void Start()
    {
        player.SetActive(false);
        //Test
        for (int i = 0; i < ItemList.Length; i++)
        {
            inventory.InventoryItems.Add(ItemList[i]);
        }
    }

    public void UpdatePlayerStats()
    {
        player.GetComponent<PlayerStatsHandler>().UpdateCharacterStats();
        OnStatusChange?.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MainScene");
        SoundManager.instance.PlayEffect("positive");
    }
}