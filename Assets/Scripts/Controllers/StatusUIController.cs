using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _attack;
    [SerializeField] private TMP_Text _defense;
    [SerializeField] private TMP_Text _maxHealth;
    [SerializeField] private TMP_Text _critical;

    private GameObject _player;

    private void Awake()
    {
        _player = GameManager.instance.player;
        Debug.Log(_player);
    }

    private void Start()
    {
        UpdateStatusText();
        GameManager.instance.OnStatusChange += UpdateStatusText;
    }

    private void UpdateStatusText()
    {
        _attack.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.attack.ToString();
        _defense.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.defense.ToString();
        _maxHealth.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.maxHealth.ToString();
        _critical.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.critical.ToString();
    }



}
