using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemUICreator : MonoBehaviour
{
    public static ItemUICreator instance;

    public Dictionary<string, GameObject> ItemTypeDict = new Dictionary<string, GameObject>();
    private List<Items> inventoryItems;
    public List<GameObject> inventoryObjects;

    public Transform InventoryPos;

    private void Awake()
    {
        instance = this;

        inventoryItems = GameManager.instance.inventory.InventoryItems;
        inventoryObjects = new List<GameObject>();

        GameObject[] loadedObjects = Resources.LoadAll<GameObject>("ItemTypes");

        foreach (var obj in loadedObjects)
        {
            ItemTypeDict[obj.name] = obj;
        }
    }

    private void Start()
    {
        CreateInventoryItems();
    }


    void CreateInventoryItems()
    {
        RectTransform rectTransform = InventoryPos as RectTransform;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 400f);

        foreach (Items item in inventoryItems)
        {
            GameObject obj = Instantiate(ItemTypeDict[$"Item_{item.Type.ToString()}"], InventoryPos);
            Image iconImg = obj.transform.GetChild(2).GetComponent<Image>();
            iconImg.sprite = item.itemIcon;
            obj.GetComponent<EquipController>().itemInfo = item;

            inventoryObjects.Add(obj);
        }

        if (inventoryItems.Count <= 9) 
        {
            for (int i = 0; i < 9 - inventoryItems.Count; i++)
            {
                GameObject obj = Instantiate(ItemTypeDict["ItemBg"], InventoryPos);
            }
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, ((float)Math.Ceiling((float)inventoryItems.Count / 3) * 130) + 10);

            if (inventoryItems.Count % 3 != 0)
            {
                for (int i = 0; i < 3 - (inventoryItems.Count % 3); i++)
                {
                    GameObject obj = Instantiate(ItemTypeDict["ItemBg"], InventoryPos);
                }
            }
        }
    }
}
