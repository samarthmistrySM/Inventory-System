using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    [SerializeField] private GameObject toOpenUi;
    [SerializeField] InventoryManager inventoryManager;

    void OnMouseDown()
    {
        if (toOpenUi != null)
        {
            toOpenUi.SetActive(true);
            Debug.Log("Activated");
            inventoryManager.OpenInventory();
        }
    }
}
