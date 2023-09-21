using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Player Info.")]
    [SerializeField] private Text _id;
    [SerializeField] private TMP_Text _desc;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _exp;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private Transform _tagPos;
    [SerializeField] private GameObject _tagPrefab;
    [SerializeField] private TMP_Text _coin;

    [Header("Status")]
    [SerializeField] private TMP_Text _attack;
    [SerializeField] private TMP_Text _defense;
    [SerializeField] private TMP_Text _maxHealth;
    [SerializeField] private TMP_Text _critical;

    private GameObject _player;

    private void Start()
    {
        _player = GameManager.instance.player;
        UpdatePlayerInfoText();
        UpdateCoin();
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

    private void UpdatePlayerInfoText()
    {
        Instantiate(_tagPrefab, _tagPos);
        _id.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.id.ToString();
        _level.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.level.ToString();
        _exp.text = string.Format($"{_player.GetComponent<PlayerStatsHandler>().CurrentStats.exp} / {_player.GetComponent<PlayerStatsHandler>().CurrentStats.maxExp}");
        _expSlider.value = (_player.GetComponent<PlayerStatsHandler>().CurrentStats.exp) / (_player.GetComponent<PlayerStatsHandler>().CurrentStats.maxExp);
        _desc.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.desc.ToString();
    }

    private void UpdateCoin()
    {
        _coin.text = _player.GetComponent<PlayerStatsHandler>().CurrentStats.coin.ToString();
    }

}
