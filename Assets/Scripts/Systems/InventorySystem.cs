using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new();
    public List<Item> ItemList = new();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
 
    public bool isOpen;

    //public bool isFull;
 
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
 
 
    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slots"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }
 
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOpen)
            {
                inventoryScreenUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                isOpen = true;
            }
            else
            {
                inventoryScreenUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                isOpen = false;
            }
        }
    }

    public void AddToInventory(Item item)
    {
        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>("InventoryObject"), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        RawImage image = itemToAdd.GetComponent<RawImage>();
        image.texture = Resources.Load<Texture>($"Sprites/{item.AssetName2D}"); // Do NOT include file extension.
        ItemList.Add(item);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int count = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                count++;
            }
        }
        if (count == 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 
}