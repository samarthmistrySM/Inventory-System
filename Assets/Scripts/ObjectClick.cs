using UnityEngine;

public class ObjectClick : MonoBehaviour
{
    [SerializeField] private GameObject toOpenUi;
    [SerializeField] private string objectName;
    [SerializeField] InventoryManager inventoryManager;

    void OnMouseDown()
    {
        if (toOpenUi != null || inventoryManager != null)
        {
            toOpenUi.SetActive(true);
            inventoryManager.OpenInventory(objectName);
        }
    }
}
