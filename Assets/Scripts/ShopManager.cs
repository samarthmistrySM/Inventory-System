using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance; //singleton instance for easy global access

    [SerializeField] private TMP_Text currencyText; //UI text to show player gold 
    [SerializeField] private int fixedPrice = 10; //fixed price for every item in shop

    public ItemSlot[] shopSlots; //slots that will display items in shop

    [SerializeField] private List<Item> items; //list of items available in shop

    private int playerCurrency = 100; //players starting gold

    void Awake()
    {
        //setup singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCurrencyUI(); //update gold UI at start

        //fill shop slots with items from list
        for (int i = 0; i < items.Count && i < shopSlots.Length; i++)
        {
            shopSlots[i].AddItem(items[i], items[i].maxStack);
        }
    }

    public void BuyItem(ItemSlot shopSlot)
    {
        if (shopSlot.item == null) return; //if slot is empty nothing to buy

        if (playerCurrency >= fixedPrice) //check if player has enough gold
        {
            playerCurrency -= fixedPrice; //deduct price
            InventoryManager.Instance.AddItemToInventory(shopSlot.item, shopSlot.item.maxStack); //add purchased item to inventory

            UpdateCurrencyUI(); //update UI
            Debug.Log("Bought " + shopSlot.item.itemName + " for " + fixedPrice);
        }
        else
        {
            Debug.Log("Not enough money!"); //not enough gold to purchase
        }
    }

    private void UpdateCurrencyUI()
    {
        //update gold display in UI
        if (currencyText != null)
            currencyText.text = "Gold: " + playerCurrency;
    }
}
