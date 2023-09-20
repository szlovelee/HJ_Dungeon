using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI Animators")]
    [SerializeField] private Animator playerInfoAnimator;
    [SerializeField] private Animator statusAnimator;
    [SerializeField] private Animator buttonsAnimator;
    [SerializeField] private Animator inventoryAnimator;
    [SerializeField] private Animator coinAnimator;



    [Header("MainCanvas")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject playerInfo;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject status;

    [Header("InventoryCanvas")]
    [SerializeField] private Canvas inventoryCanvas;

    private static readonly int statusShow = Animator.StringToHash("statusShow");
    private static readonly int buttonsShow = Animator.StringToHash("buttonsShow");
    private static readonly int inventoryShow = Animator.StringToHash("inventoryShow");
    private static readonly int playerInfoShow = Animator.StringToHash("playerInfoShow");
    private static readonly int coinShow = Animator.StringToHash("coinShow");



    private void Start()
    {
        inventoryCanvas.gameObject.SetActive(false);
        status.SetActive(false);
    }

    public void OpenStatus()
    {
        StartCoroutine(ToggleGameObjectWithDelay(buttons, false));
        status.SetActive(true);
        statusAnimator.SetBool(statusShow, true);
        buttonsAnimator.SetBool(buttonsShow, false);

    }

    public void CloseStatus()
    {
        StartCoroutine(ToggleGameObjectWithDelay(status, false));
        buttons.SetActive(true);
        statusAnimator.SetBool(statusShow, false);
        buttonsAnimator.SetBool(buttonsShow, true);
    }

    public void OpenInventory()
    {
        StartCoroutine(ToggleGameObjectWithDelay(mainCanvas.gameObject, false));
        inventoryCanvas.gameObject.SetActive(true);
        buttonsAnimator.SetBool(buttonsShow, false);
        playerInfoAnimator.SetBool(playerInfoShow, false);
        inventoryAnimator.SetBool(inventoryShow, true);
        coinAnimator.SetBool(coinShow, false);

    }

    public void CloseInventory()
    {
        StartCoroutine(ToggleGameObjectWithDelay(inventoryCanvas.gameObject, false));
        mainCanvas.gameObject.SetActive(true);
        coinAnimator.SetBool(coinShow, true);
        inventoryAnimator.SetBool(inventoryShow, false);
    }

    IEnumerator ToggleGameObjectWithDelay(GameObject obj, bool state, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(state);
    }
}
