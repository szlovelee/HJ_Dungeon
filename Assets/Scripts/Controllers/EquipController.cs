using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    private static GameObject[] Pos;
    private GameObject WeaponPos;
    private GameObject ShieldPos;
    private GameObject ArmorPos;
    private GameObject RingPos;

    private static Sprite emptyImg;

    public Items itemInfo;
    public static Items[] Equipped;
    public Button inventoryButton;

    private static List<GameObject> inventoryObjects;
    private static GameObject[] EquippedObjects = new GameObject[4];
    private static GameObject selectedItem;
    private static Items selectedItemInfo;

    private void Start()
    {
        inventoryObjects = ItemUICreator.instance.inventoryObjects;
        Equipped = GameManager.instance.inventory.EquippedItems;

        WeaponPos = MainUIController.instance.WeaponPos;
        ShieldPos = MainUIController.instance.ShieldPos;
        ArmorPos = MainUIController.instance.ArmorPos;
        RingPos = MainUIController.instance.RingPos;
        
        Pos = new GameObject[] { WeaponPos, ShieldPos, ArmorPos, RingPos }; //ItemType enum ¼ø¼­

        emptyImg = MainUIController.instance.emptyImg;

        inventoryButton = GetComponent<Button>();
        inventoryButton.onClick.AddListener(ItemSelection);

        MainUIController.instance.OnChangeConfirm += ChangeEquipped;

        UpdateEquippedUI();
    }

    private static void UpdateEquippedUI()
    {
        for (int i = 0; i <  Equipped.Length; i++)
        {
            if (Equipped[i] == null)
            {
                ResetImageFormat(Pos[i]);
                continue;
            }

            Pos[i].GetComponent<Image>().sprite = Equipped[i].itemIcon;
            SetImageFormat(Pos[i]);                
        }

        for (int j = 0; j < inventoryObjects.Count; j++)
        {
            inventoryObjects[j].transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int k = 0; k < EquippedObjects.Length; k++)
        {
            if (EquippedObjects[k] != null)
                EquippedObjects[k].transform.GetChild(0).gameObject.SetActive(true);
        }
        GameManager.instance.UpdatePlayerStats();
    }

    private void ItemSelection()
    {
        if (Equipped[(int)itemInfo.Type] != itemInfo)
        {
            selectedItem = this.gameObject;
            selectedItemInfo = itemInfo;
            MainUIController.instance.OpenChangeConfirm(selectedItemInfo);
            SoundManager.instance.PlayEffect("option");
        }
        else
        {
            RemoveEquipped();
            UpdateEquippedUI();
        }
    }

    private void RemoveEquipped()
    {
        Equipped[(int)itemInfo.Type] = null;
        EquippedObjects[(int)itemInfo.Type] = null;
        SoundManager.instance.PlayEffect("remove");
    }

    private void ChangeEquipped()
    {
        Equipped[(int)selectedItem.GetComponent<EquipController>().itemInfo.Type] = selectedItem.GetComponent<EquipController>().itemInfo;
        EquippedObjects[(int)selectedItem.GetComponent<EquipController>().itemInfo.Type] = selectedItem.gameObject;
        UpdateEquippedUI();
        SoundManager.instance.PlayEffect("equip");
    }

    private static void SetImageFormat(GameObject pos)
    {
        pos.GetComponent<Image>().color = new Color32(255, 255, 255, 180);

        RectTransform rectTransform = pos.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    private static void ResetImageFormat(GameObject pos)
    {
        pos.GetComponent<Image>().color = new Color32(255, 255, 255, 24);
        pos.GetComponent<Image>().sprite = emptyImg;

        RectTransform rectTransform = pos.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
}
