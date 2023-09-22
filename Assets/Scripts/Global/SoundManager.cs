using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioClip _bgm;
    [SerializeField] private AudioClip positive;
    [SerializeField] private AudioClip negative;
    [SerializeField] private AudioClip equip;
    [SerializeField] private AudioClip remove;
    [SerializeField] private AudioClip option;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBGM();
    }

    private void PlayBGM()
    {
        _bgmSource.clip = _bgm;
        _bgmSource.volume = 0.5f;
        _bgmSource.Play();
    }

    public void PlayEffect(string effectName)
    {
        _effectSource.volume = 0.7f;

        switch (effectName)
        {
            case "positive":
                _effectSource.PlayOneShot(positive);
                break;

            case "negative":
                _effectSource.PlayOneShot(negative);
                break;

            case "equip":
                _effectSource.volume = 0.1f;
                _effectSource.PlayOneShot(equip);
                break;

            case "remove":
                _effectSource.PlayOneShot(remove);
                break;
            case "option":
                _effectSource.volume = 0.5f;
                _effectSource.PlayOneShot(option);
                break;
        }
    }
}
