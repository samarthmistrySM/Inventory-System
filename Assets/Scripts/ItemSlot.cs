using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler,
    IDropHandler
{
    [Header("Item Info")]
    public Item item;
    public int quantity;
    public bool isFull;

    public enum SlotType
    {
        Inventory,
        Chest,
        Shop,
        CraftingIngregient,
        CraftingOutput
    }

    public SlotType slotType;

    [Header("Item Ui Info")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    [Header("Selection Section")]
    public GameObject selectedItem;
    public bool isSlotSelected;

    private Canvas canvas;
    private GameObject draggedIcon;
    private RectTransform draggedRect;
    private bool isDragging;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public int AddItem(Item _item, int _quantity)
    {
        if (_item == null || _quantity <= 0) return _quantity;

        if (item == null)
        {
            item = _item;
            quantity = Mathf.Min(_quantity, _item.maxStack);
            isFull = (quantity >= _item.maxStack);
            UpdateUI();
            return _quantity - quantity;
        }

        if (item == _item && item.isStackable)
        {
            int spaceLeft = item.maxStack - quantity;
            int toAdd = Mathf.Min(spaceLeft, _quantity);
            quantity += toAdd;
            isFull = (quantity >= item.maxStack);
            UpdateUI();
            return _quantity - toAdd;
        }

        return _quantity;
    }

    public void UpdateUI()
    {
        if (item != null)
        {
            itemImage.sprite = item.icon;
            itemImage.enabled = true;

            if (item.isStackable)
            {
                quantityText.text = quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
            else
            {
                quantityText.gameObject.SetActive(false);
            }
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
            quantityText.gameObject.SetActive(false);
        }
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
        isFull = false;
        UpdateUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick();
    }

    void OnRightClick()
    {
        Debug.Log("Right click not implemented");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedItem != null)
            selectedItem.SetActive(true);

        isSlotSelected = true;

        InventoryManager.Instance.ShowDescription(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedItem != null)
            selectedItem.SetActive(false);

        isSlotSelected = false;

        InventoryManager.Instance.ShowDescription(null);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || item == null) return;

        isDragging = true;
        eventData.pointerDrag = gameObject;

        draggedIcon = new GameObject("DraggedIcon");
        draggedIcon.transform.SetParent(canvas.transform, false);
        draggedIcon.transform.SetAsLastSibling();

        Image img = draggedIcon.AddComponent<Image>();
        img.sprite = item.icon;
        img.raycastTarget = false;

        draggedRect = draggedIcon.GetComponent<RectTransform>();
        draggedRect.sizeDelta = new Vector2(100, 100);
        draggedRect.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && draggedRect != null)
            draggedRect.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging || eventData.button != PointerEventData.InputButton.Left) return;

        isDragging = false;
        if (draggedIcon != null)
            Destroy(draggedIcon);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (draggedSlot == null || draggedSlot == this) return;

        if (draggedSlot.slotType == SlotType.Shop && slotType == SlotType.Inventory)
        {
            if (ShopManager.Instance != null)
            {
                ShopManager.Instance.BuyItem(draggedSlot);
            }
            else
            {
                Debug.LogWarning("No ShopManager in scene!");
            }
            return;
        }

        SwapItems(draggedSlot);
    }


    private void SwapItems(ItemSlot other)
    {
        if (item != null && other.item != null && item == other.item && item.isStackable)
        {
            int space = item.maxStack - quantity;
            if (space > 0)
            {
                int transfer = Mathf.Min(space, other.quantity);
                quantity += transfer;
                other.quantity -= transfer;

                isFull = (quantity >= item.maxStack);
                other.isFull = (other.quantity >= other.item.maxStack);

                if (other.quantity <= 0) other.ClearSlot();

                UpdateUI();
                other.UpdateUI();
            }
            return;
        }

        Item oldItem = item;
        int oldQty = quantity;
        bool oldFull = isFull;

        item = other.item;
        quantity = other.quantity;
        isFull = other.isFull;

        other.item = oldItem;
        other.quantity = oldQty;
        other.isFull = oldFull;

        UpdateUI();
        other.UpdateUI();
    }
}
