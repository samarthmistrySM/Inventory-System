using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Database")]
public class ItemDatabase : ScriptableObject
{
    [Header("All registered items")]
    public List<Item> allItems;

    public Item GetItemByName(string itemName)
    {
        foreach (var item in this.allItems)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
