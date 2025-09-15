using UnityEngine;
using TMPro; // for TextMeshProUGUI

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    [SerializeField] private ItemSlot[] hotbarSlots;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI weaponText;

    private int selectedIndex = 0;
    private int baseHealth = 100;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SelectSlot(0);
        UpdateStatsUI();
    }

    void Update()
    {
        UpdateStatsUI();
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SelectSlot((selectedIndex + 1) % hotbarSlots.Length);
        if (scroll < 0f) SelectSlot((selectedIndex - 1 + hotbarSlots.Length) % hotbarSlots.Length);
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length) return;

        selectedIndex = index;

        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].selectedItem.SetActive(i == selectedIndex);
        }

        Debug.Log($"Hotbar selected: {hotbarSlots[selectedIndex].item?.itemName}");

        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        int armorCount = 0;
        foreach (var slot in hotbarSlots)
        {
            if (slot.item != null && slot.item.category == ItemCategory.Armour)
            {
                armorCount++;
            }
        }

        int armorValue = armorCount * 5;

        if (healthText != null) healthText.text = $"Health: {baseHealth}";
        if (armorText != null) armorText.text = $"Armor: {armorValue}";
        if (hotbarSlots[selectedIndex].item != null && hotbarSlots[selectedIndex].item.category == ItemCategory.Weapon)
        {
            weaponText.text = $"Weapon: {hotbarSlots[selectedIndex].item.itemName}";
        }
        else
        {
            weaponText.text = "Weapon: None";
        }

    }

    public Item GetSelectedItem()
    {
        return hotbarSlots[selectedIndex].item;
    }
}
