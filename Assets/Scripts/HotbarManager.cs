using System.Collections.Generic;
using UnityEngine;
using TMPro; // for TextMeshProUGUI

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    [SerializeField] private List<ItemSlot> hotbarSlots; //all the slots which are used in hotbar

    //Ui Text Variables
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI weaponText;

    private int selectedIndex = 0; //currently selected slot
    private int baseHealth = 100; //health of player

    void Awake()
    {
        Instance = this; //singleton pattern as it exist only one
    }

    void Start()
    {
        //initial state of player
        SelectSlot(0);
        UpdateStatsUI();
    }

    void Update()
    {
        UpdateStatsUI();
        //numric keys to select slot
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
        //scroll to select slot
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) SelectSlot((selectedIndex + 1) % hotbarSlots.Count);
        if (scroll < 0f) SelectSlot((selectedIndex - 1 + hotbarSlots.Count) % hotbarSlots.Count);
    }

    public void SelectSlot(int index)
    {
        //update the ui for selected item
        if (index < 0 || index >= hotbarSlots.Count) return;

        selectedIndex = index;
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].selectedItem.SetActive(i == selectedIndex);
        }

        Debug.Log($"Hotbar selected: {hotbarSlots[selectedIndex].item?.itemName}");

        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        //for calulation of total armour points that user has equiped
        int armorCount = 0;
        foreach (var slot in hotbarSlots)
        {
            if (slot.item != null && slot.item.category == ItemCategory.Armour)
            {
                armorCount++;
            }
        }

        //updating ui utilities like health, armour and weapoin

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
