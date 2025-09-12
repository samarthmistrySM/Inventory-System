using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryMenu;
    [SerializeField] private GameObject DescriptionContainer;
    [SerializeField] private GameObject ChestContainer;
    private bool menuActivated;
    public ItemSlot[] itemSlots;

    [Header("Testing Inventory")]
    public Item[] testItems;
    public int[] testQuantities;

    void Start()
    {
        if (testItems != null && testQuantities != null)
        {
            int count = testItems.Length;
            for (int i = 0; i < count; i++)
            {
                if (testItems[i] != null)
                {
                    AddItem(testItems[i], testQuantities[i]);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            InventoryMenu.SetActive(false);
            ChestContainer.SetActive(false);
            menuActivated = false;
            DescriptionContainer.SetActive(false);
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            ChestContainer.SetActive(false);
            menuActivated = true;
            DescriptionContainer.SetActive(true);
        }
    }

    public void OpenInventory()
    {
        InventoryMenu.SetActive(true);
        menuActivated = true;
    }

    public void AddItem(Item item, int quantity)
    {
        int remaining = quantity;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item && item.isStackable && !itemSlots[i].isFull)
            {
                remaining = itemSlots[i].AddItem(item, remaining);
                if (remaining <= 0) return;
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                remaining = itemSlots[i].AddItem(item, remaining);
                if (remaining <= 0) return;
            }
        }

        if (remaining > 0)
        {
            Debug.Log("Inventory si full Item: " + item.itemName + " with remaining quantity: " + remaining + " coundnt added!");
        }
    }
}
