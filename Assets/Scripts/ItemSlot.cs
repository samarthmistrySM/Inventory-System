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
    public Item item;
    public int quantity;
    public bool isFull;

    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

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
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void AddItem(Item _item, int _quantity)
    {
        this.item = _item;
        this.quantity = _quantity;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.gameObject.SetActive(true);
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
        isFull = false;

        quantityText.gameObject.SetActive(false);
        itemImage.sprite = null;
        itemImage.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    void OnRightClick()
    {
        Debug.Log("Right click not implemented");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedItem.SetActive(true);
        isSlotSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectedItem.SetActive(false);
        isSlotSelected = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && isFull)
        {
            isDragging = true;

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
        ItemSlot draggedSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (draggedSlot != null && draggedSlot != this)
        {
            SwapItems(draggedSlot);
        }
    }

    void SwapItems(ItemSlot otherSlot)
    {
        Item tempItem = item;
        int tempQuantity = quantity;

        if (otherSlot.isFull)
        {
            AddItem(otherSlot.item, otherSlot.quantity);
        }
        else
        {
            ClearSlot();
        }

        if (tempItem != null)
        {
            otherSlot.AddItem(tempItem, tempQuantity);
        }
        else
        {
            otherSlot.ClearSlot();
        }
    }
}
