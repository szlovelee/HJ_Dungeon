using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUIController : UIController
{
    public static IntroUIController instance;

    [Header("Intro Scene")]
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private GameObject startBtn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transitionPanel.SetActive(false);
        startBtn.SetActive(false);
        GameManager.instance.player.SetActive(false);
        StartCoroutine(ToggleGameObjectWithDelay(startBtn, true, 3f));
    }

    public void TransitionStart()
    {
        transitionPanel.SetActive(true);
    }


}
