using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; //singleton reference

    [Header("UI Panels")]
    [SerializeField] private GameObject InventoryMenu; //main inventory UI
    [SerializeField] private GameObject ChestContainer; //UI for chest items
    [SerializeField] private GameObject DescriptionContainer; //UI for item details
    [SerializeField] private GameObject ShopContainer; //UI for shop
    [SerializeField] private GameObject CraftingContainer; //UI for crafting

    [Header("Description UI")]
    [SerializeField] private Image descriptionImg; //icon shown in description
    [SerializeField] private TMP_Text descriptionName; //name of the item
    [SerializeField] private TMP_Text descriptionCategory; //category of the item

    [Header("Slots")]
    public List<ItemSlot> itemSlots; //player inventory slots
    public List<ItemSlot> chestSlots; //chest slots

    [Header("Testing")]
    public List<Item> testItems; //items for testing
    public List<int> testQuantities; //quantities for testing

    //booleans for menu toggle
    private bool menuActivated;
    private bool chestActivated;
    private bool shopActivated;
    private bool craftingActivated;

    void Awake()
    {
        Instance = this; //singleton set
    }

    void Start()
    {
        //fill test items into inventory at start
        if (testItems != null && testQuantities != null)
        {
            for (int i = 0; i < testItems.Count; i++)
            {
                if (testItems[i] != null)
                    AddItemToInventory(testItems[i], testQuantities[i]);
            }
        }
    }

    //open inventory and enable a specific submenu
    public void OpenInventory(string toOpenUiName)
    {
        InventoryMenu.SetActive(true);
        menuActivated = true;

        //reset all submenus
        ShopContainer.SetActive(false); shopActivated = false;
        CraftingContainer.SetActive(false); craftingActivated = false;
        ChestContainer.SetActive(false); chestActivated = false;

        //open requested UI
        if (toOpenUiName.Equals("Shop"))
        {
            ShopContainer.SetActive(true); shopActivated = true;
        }
        else if (toOpenUiName.Equals("Crafting"))
        {
            CraftingContainer.SetActive(true); craftingActivated = true;
        }
        else
        {
            ChestContainer.SetActive(true); chestActivated = true;
        }
    }

    void Update()
    {
        //close if open
        if ((Input.GetButtonDown("Inventory") || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && menuActivated)
        {
            InventoryMenu.SetActive(false);
            ChestContainer.SetActive(false);
            DescriptionContainer.SetActive(false);
            CraftingContainer.SetActive(false);
            menuActivated = false;
            chestActivated = false;
            shopActivated = false;
            craftingActivated = false;
        }
        //open fresh
        else if (Input.GetButtonDown("Inventory") || Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryMenu.SetActive(true);
            DescriptionContainer.SetActive(true);
            ChestContainer.SetActive(false);
            ShopContainer.SetActive(false);
            CraftingContainer.SetActive(false);
            menuActivated = true;
            chestActivated = false;
            shopActivated = false;
            craftingActivated = false;
        }
    }

    //show description for selected item
    public void ShowDescription(Item item)
    {
        if (item == null || chestActivated || shopActivated || craftingActivated)
        {
            DescriptionContainer.SetActive(false);
            return;
        }

        ChestContainer.SetActive(false);
        ShopContainer.SetActive(false);
        CraftingContainer.SetActive(false);

        DescriptionContainer.SetActive(true);
        descriptionImg.sprite = item.icon;
        descriptionName.text = item.itemName;
        descriptionCategory.text = item.category.ToString();
    }

    //add item into inventory
    public void AddItemToInventory(Item item, int quantity)
    {
        int remaining = quantity;

        //stack onto existing
        foreach (var slot in itemSlots)
        {
            if (slot.item == item && item.isStackable && !slot.isFull)
                remaining = slot.AddItem(item, remaining);

            if (remaining <= 0) return;
        }

        //fill empty slots
        foreach (var slot in itemSlots)
        {
            if (slot.item == null)
                remaining = slot.AddItem(item, remaining);

            if (remaining <= 0) return;
        }

        //inventory full
        if (remaining > 0)
            Debug.Log("Inventory full: " + item.itemName + " (remaining: " + remaining + ")");
    }
}
