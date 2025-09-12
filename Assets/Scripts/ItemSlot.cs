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

    [Header("Item Ui Info")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    [Header("Description Section")]
    [SerializeField] private Image itemDescriptionImg;
    [SerializeField] private TMP_Text itemDescriptionName;
    [SerializeField] private TMP_Text itemDescriptionCategory;

    [Header("Selection Section")]
    public GameObject selectedItem;
    public bool isSlotSelected;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

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
        selectedItem.SetActive(true);
        itemImage.raycastTarget = false;
        isSlotSelected = true;
        itemDescriptionImg.sprite = item.icon;
        itemDescriptionName.text = item.itemName;
        itemDescriptionCategory.text = item.category.ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selectedItem) selectedItem.SetActive(false);
        isSlotSelected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item != null)
        {
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && draggedRect != null)
        {
            draggedRect.position = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging && eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;

            if (draggedIcon != null)
            {
                Destroy(draggedIcon);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (draggedSlot != null && draggedSlot != this)
        {
            SwapItems(draggedSlot);
        }
    }

    private void SwapItems(ItemSlot other)
    {
        if (item != null && other.item != null && item == other.item && item.isStackable)
        {
            int availableSpace = item.maxStack - quantity;
            if (availableSpace > 0)
            {
                int amountToTransfer = Mathf.Min(availableSpace, other.quantity);

                quantity += amountToTransfer;
                other.quantity -= amountToTransfer;

                isFull = (quantity >= item.maxStack);

                if (other.quantity <= 0)
                    other.ClearSlot();
                else
                    other.isFull = (other.quantity >= other.item.maxStack);

                UpdateUI();
                other.UpdateUI();
            }
            return;
        }

        Item oldItem = item;
        int oldQuantity = quantity;
        bool oldIsFull = isFull;

        item = other.item;
        quantity = other.quantity;
        isFull = other.isFull;

        other.item = oldItem;
        other.quantity = oldQuantity;
        other.isFull = oldIsFull;

        UpdateUI();
        other.UpdateUI();
    }

}
