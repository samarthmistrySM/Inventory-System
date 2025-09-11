using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
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
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
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
