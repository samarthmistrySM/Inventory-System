using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance;

    [SerializeField] private ItemSlot[] hotbarSlots;
    private int selectedIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SelectSlot(0);
    }


    void Update()
    {
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
    }

    public Item GetSelectedItem()
    {
        return hotbarSlots[selectedIndex].item;
    }
}
