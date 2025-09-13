using System.Collections;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private CraftingRecipe[] recipes;
    [SerializeField] private ItemSlot[] inputSlots;
    [SerializeField] private ItemSlot outputSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TryCraft();
        }
    }

    private bool MatchesRecipe(CraftingRecipe recipe)
    {
        for (int i = 0; i < inputSlots.Length; i++)
        {
            Item required = recipe.inputs[i];
            Item inSlot = inputSlots[i].item;

            if (required == null && inSlot == null) continue;
            if (required == null && inSlot != null) return false;
            if (required != null && inSlot == null) return false;
            if (required != inSlot) return false;
        }
        return true;
    }


    private void Craft(CraftingRecipe recipe)
    {
        for (int i = 0; i < recipe.inputs.Length; i++)
        {
            if (recipe.inputs[i] != null)
            {
                ItemSlot slot = inputSlots[i];
                slot.quantity -= 1;

                if (slot.quantity <= 0) slot.ClearSlot();
                else slot.UpdateUI();
            }
        }
        outputSlot.AddItem(recipe.resultItem, recipe.resultQuantity);
    }

    public void TryCraft()
    {
        foreach (var recipe in recipes)
        {
            if (MatchesRecipe(recipe))
            {
                Craft(recipe);
                return;
            }
        }
        Debug.Log("recipe not found!");
    }

    public void ReturnInputsToInventory()
    {
        foreach (var slot in inputSlots)
        {
            if (slot.item != null && slot.quantity > 0)
            {
                InventoryManager.Instance.AddItemToInventory(slot.item, slot.quantity);
                slot.ClearSlot();
            }
        }
    }

    void OnDisable()
    {
        ReturnInputsToInventory();
    }

}
