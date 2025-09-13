using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private int fixedPrice = 10;

    public ItemSlot[] shopSlots;
    [SerializeField] private Item[] items;

    private int playerCurrency = 100;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
#if UNITY_EDITOR
            DestroyImmediate(gameObject);
#else
        Destroy(gameObject);
#endif
        }
    }


    void Start()
    {
        UpdateCurrencyUI();

        for (int i = 0; i < items.Length; i++)
        {
            shopSlots[i].AddItem(items[i], items[i].maxStack);
        }
    }

    public void BuyItem(ItemSlot shopSlot)
    {
        if (shopSlot.item == null) return;

        if (playerCurrency >= fixedPrice)
        {
            playerCurrency -= fixedPrice;
            InventoryManager.Instance.AddItemToInventory(shopSlot.item, shopSlot.item.maxStack);

            UpdateCurrencyUI();
            Debug.Log("Bought " + shopSlot.item.itemName + " for " + fixedPrice);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
            currencyText.text = "Gold: " + playerCurrency;
    }
}
