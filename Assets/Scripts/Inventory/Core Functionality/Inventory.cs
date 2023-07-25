using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory : Initializable
{
    [HideInInspector] public List<InventorySlot> ItemSlots;
    public int InventorySize;
    public InventoryUIComponent UIComponent;
    [SerializeField] Item itemToAdd;
    [SerializeField] Item secondItemToAdd;
    
    Vector2Int hoveredSlotPosition;
    InventorySlot selectedSlot;
    InventorySlot hoveredSlot;

    public override void Initialize()
    {
        //There is an underlying error here that we need to address
        if (UIComponent == null) return;
        InitializeInventory();
        UIComponent.gameObject.SetActive(false);
    }

    public void InitializeInventory()
    {
        ItemSlots = new List<InventorySlot>(InventorySize);
        UIComponent.Initialize(ItemSlots);
        for(int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].ParentInventory = this;
        }
        ItemSlots[0].Item = itemToAdd;
        ItemSlots[0].SlotIconComponent.sprite = ItemSlots[0].Item.ItemSprite;
        ItemSlots[0].SlotIconComponent.color = Color.white;

        ItemSlots[5].Item = secondItemToAdd;
        ItemSlots[5].SlotIconComponent.sprite = ItemSlots[5].Item.ItemSprite;
        ItemSlots[5].SlotIconComponent.color = Color.white;
        hoveredSlot = ItemSlots[0];
        hoveredSlotPosition = Vector2Int.zero;
    }

    public void OpenInventory()
    {
        InputManager.ChangeActionMap(InputManager.InputActionSet.Inventory);
        UIComponent.gameObject.SetActive(true);
        BindKeys();
    }
    
    public void CloseInventory()
    {
        InputManager.ChangeActionMap(InputManager.InputActionSet.Combat);
        UnbindKeys();
        UIComponent.gameObject.SetActive(false);
    }

    public void AddItemToInventory(Item itemToAdd, int count)
    {
        InventorySlot emptySlot = FindFirstEmptySlot();
        if(emptySlot == null)
        {
            Debug.LogWarning("Inventory is already full");
            return;
        }
        emptySlot.Item = itemToAdd;
        emptySlot.StackAmount = count;
    }

    public InventorySlot TakeSelectedItemFromInventory()
    {
        selectedSlot.Item = null;
        selectedSlot.StackAmount = 0;
        selectedSlot = null;
        return selectedSlot;
    }
    InventorySlot FindFirstEmptySlot()
    {
        for(int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].Item == null)
            {
                return ItemSlots[i];
            }
        }


        return null;
    }

    void BindKeys()
    {
        if (UIComponent == null) return;
        InputManager.InputActionSet.Inventory.Move.performed += MoveMenuCursor;
        InputManager.InputActionSet.Inventory.Select.performed += SelectItem;
    }
    
    void UnbindKeys()
    {
        InputManager.InputActionSet.Inventory.Move.performed -= MoveMenuCursor;
        InputManager.InputActionSet.Inventory.Select.performed -= SelectItem;
    }
    
    void MoveMenuCursor(InputAction.CallbackContext context)
    {
        Vector2 movementDirection = context.ReadValue<Vector2>();
        hoveredSlotPosition = new Vector2Int(hoveredSlotPosition.x + (int) movementDirection.x, hoveredSlotPosition.y - (int) movementDirection.y);
        //hoveredSlot = ItemSlots[hoveredSlotPosition.y][hoveredSlotPosition.x];
    }
    
    public void SwapItemsInSlots(InventorySlot from, InventorySlot to)
    {
        Item tempItem = from.Item;
        Inventory tempParentInventory = from.ParentInventory;
        Sprite tempSprite = from.SlotIconComponent.sprite;
        Color tempSpriteColor = from.SlotIconComponent.color;
        int tempStackAmount = from.StackAmount;

        from.Item = to.Item;
        from.ParentInventory = to.ParentInventory;
        from.SlotIconComponent.sprite = to.SlotIconComponent.sprite;
        from.SlotIconComponent.color = to.SlotIconComponent.color;
        from.StackAmount = to.StackAmount;

        to.Item = tempItem;
        to.ParentInventory = tempParentInventory;
        to.SlotIconComponent.sprite = tempSprite;
        to.SlotIconComponent.color = tempSpriteColor;
        to.StackAmount = tempStackAmount;

    }
    
    void SelectItem(InputAction.CallbackContext context)
    {
        if (selectedSlot == null) selectedSlot = hoveredSlot;
        else
        {
            SwapItemsInSlots(selectedSlot, hoveredSlot);
            selectedSlot = null;
        }
    }
    public override void Start()
    {
    }
}
