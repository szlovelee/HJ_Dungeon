using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : UIController
{
    public static MainUIController instance;

    [Header("UI Animators")]
    [SerializeField] private Animator playerInfoAnimator;
    [SerializeField] private Animator statusAnimator;
    [SerializeField] private Animator buttonsAnimator;
    [SerializeField] private Animator inventoryAnimator;
    [SerializeField] private Animator coinAnimator;
    [SerializeField] private Animator confirmAnimator;

    [Header("Main Canvas")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject playerInfo;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject status;
    [SerializeField] private GameObject startPanel;

    [Header("Inventory Canvas")]
    [SerializeField] private Canvas inventoryCanvas;
    public GameObject WeaponPos;
    public GameObject ShieldPos;
    public GameObject ArmorPos;
    public GameObject RingPos;
    public Sprite emptyImg;

    [Header("Confirm Panel")]
    [SerializeField] private GameObject changeConfirm;
    [SerializeField] private Text itemName;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Transform ItemIconPos;
    [SerializeField] private Transform EffectPos;

    private static readonly int statusShow = Animator.StringToHash("statusShow");
    private static readonly int buttonsShow = Animator.StringToHash("buttonsShow");
    private static readonly int inventoryShow = Animator.StringToHash("inventoryShow");
    private static readonly int playerInfoShow = Animator.StringToHash("playerInfoShow");
    private static readonly int coinShow = Animator.StringToHash("coinShow");
    private static readonly int confirmShow = Animator.StringToHash("confirmShow");

    public event Action OnChangeConfirm;
    public Dictionary<string, GameObject> ItemTypeDict = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> ItemEffectDict = new Dictionary<string, GameObject>();


    private void Awake()
    {
        instance = this;

        GameObject[] loadedItemTypePrefab = Resources.LoadAll<GameObject>("ItemTypes");
        GameObject[] loadedItemEffectPrefab = Resources.LoadAll<GameObject>("ItemEffects");

        foreach (var obj in loadedItemTypePrefab)
        {
            ItemTypeDict[obj.name] = obj;
        }

        foreach (var obj in loadedItemEffectPrefab)
        {
            ItemEffectDict[obj.name] = obj;
        }
    }

    private void Start()
    {
        GameManager.instance.player.SetActive(true);
        inventoryCanvas.gameObject.SetActive(false);
        changeConfirm.SetActive(false);
        status.SetActive(false);
        StartCoroutine(ToggleGameObjectWithDelay(startPanel, false, 0.5f));
    }

    public void OpenStatus()
    {
        StartCoroutine(ToggleGameObjectWithDelay(buttons, false));
        status.SetActive(true);
        statusAnimator.SetBool(statusShow, true);
        buttonsAnimator.SetBool(buttonsShow, false);
        SoundManager.instance.PlayEffect("positive");

    }

    public void CloseStatus()
    {
        StartCoroutine(ToggleGameObjectWithDelay(status, false));
        buttons.SetActive(true);
        statusAnimator.SetBool(statusShow, false);
        buttonsAnimator.SetBool(buttonsShow, true);
        SoundManager.instance.PlayEffect("negative");
    }

    public void OpenInventory()
    {
        StartCoroutine(ToggleGameObjectWithDelay(mainCanvas.gameObject, false));
        inventoryCanvas.gameObject.SetActive(true);
        buttonsAnimator.SetBool(buttonsShow, false);
        playerInfoAnimator.SetBool(playerInfoShow, false);
        inventoryAnimator.SetBool(inventoryShow, true);
        coinAnimator.SetBool(coinShow, false);
        SoundManager.instance.PlayEffect("positive");
    }

    public void CloseInventory()
    {
        StartCoroutine(ToggleGameObjectWithDelay(inventoryCanvas.gameObject, false));
        mainCanvas.gameObject.SetActive(true);
        coinAnimator.SetBool(coinShow, true);
        inventoryAnimator.SetBool(inventoryShow, false);
        SoundManager.instance.PlayEffect("negative");
    }

    public void OpenChangeConfirm(Items selected)
    {
        foreach (Transform child in ItemIconPos)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in EffectPos)
        {
            Destroy(child.gameObject);
        }

        GameObject icon = Instantiate(ItemTypeDict[$"Item_{selected.Type.ToString()}"], ItemIconPos);
        Image iconImg = icon.transform.GetChild(2).GetComponent<Image>();
        iconImg.sprite = selected.itemIcon;

        GameObject effect = Instantiate(ItemEffectDict[$"Effect_{selected.Type.ToString()}"], EffectPos);
        Text effectTxt = effect.transform.GetChild(2).GetComponent<Text>();
        effectTxt.text = (selected.attack + selected.defense + selected.maxHealth + selected.critical).ToString();

        itemName.text = selected.itemName;

        if (GameManager.instance.inventory.EquippedItems[(int)selected.Type] != null)
        {
            warningText.text = $"현재 {GameManager.instance.inventory.EquippedItems[(int)selected.Type].itemName}을/를 착용 중입니다.";
        }
        else
        {
            warningText.text = "";
        }

        changeConfirm.SetActive(true);
        StartCoroutine(TimeScaleControlDelay(0f, 0.3f));
    }

    public void CancelChangeConfirm()
    {
        Time.timeScale = 1f;
        confirmAnimator.SetBool(confirmShow, false);
        StartCoroutine(ToggleGameObjectWithDelay(changeConfirm.gameObject, false, 0.3f));
        SoundManager.instance.PlayEffect("negative");
    }

    public void CloseChangeConfirm()
    {
        Time.timeScale = 1f;
        confirmAnimator.SetBool(confirmShow, false);
        StartCoroutine(ToggleGameObjectWithDelay(changeConfirm.gameObject, false, 0.3f));
        OnChangeConfirm?.Invoke();
    }
}
