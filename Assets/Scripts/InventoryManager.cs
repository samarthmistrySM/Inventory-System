using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject InventoryMenu;
    [SerializeField] private GameObject ChestContainer;
    [SerializeField] private GameObject DescriptionContainer;
    [SerializeField] private GameObject ShopContainer;
    [SerializeField] private GameObject CraftingContainer;

    [Header("Description UI")]
    [SerializeField] private Image descriptionImg;
    [SerializeField] private TMP_Text descriptionName;
    [SerializeField] private TMP_Text descriptionCategory;

    [Header("Slots")]
    public ItemSlot[] itemSlots;
    public ItemSlot[] chestSlots;

    [Header("Testing")]
    public Item[] testItems;
    public int[] testQuantities;
    private bool menuActivated;
    private bool chestActivated;
    private bool shopActivated;
    private bool craftingActivated;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (testItems != null && testQuantities != null)
        {
            for (int i = 0; i < testItems.Length; i++)
                if (testItems[i] != null)
                    AddItemToInventory(testItems[i], testQuantities[i]);
        }
    }
    public void OpenInventory(string toOpenUiName)
    {
        InventoryMenu.SetActive(true);
        menuActivated = true;

        ShopContainer.SetActive(false);
        shopActivated = false; ;
        CraftingContainer.SetActive(false);
        craftingActivated = false;
        ChestContainer.SetActive(false);
        chestActivated = false;

        if (toOpenUiName.Equals("Shop"))
        {
            ShopContainer.SetActive(true);
            shopActivated = true;
        }
        else if (toOpenUiName.Equals("Crafting"))
        {
            CraftingContainer.SetActive(true);
            craftingActivated = true;
        }
        else
        {
            ChestContainer.SetActive(true);
            chestActivated = true;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
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
        else if (Input.GetButtonDown("Inventory"))
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

    public void AddItemToInventory(Item item, int quantity)
    {
        int remaining = quantity;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item && item.isStackable && !itemSlots[i].isFull)
                remaining = itemSlots[i].AddItem(item, remaining);


            if (remaining <= 0) return;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
                remaining = itemSlots[i].AddItem(item, remaining);

            if (remaining <= 0) return;
        }

        if (remaining > 0)
            Debug.Log("Inventory full: " + item.itemName + " (remaining: " + remaining + ")");
    }
}
