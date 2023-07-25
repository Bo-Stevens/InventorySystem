using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory : Initializable
{
    public int InventorySize;
    public InventoryUIComponent UIComponent;
    [SerializeField] Item itemToAdd;
    [SerializeField] Item secondItemToAdd;
    
    Vector2Int hoveredSlotPosition;
    InventorySlot selectedSlot;
    InventorySlot hoveredSlot;
    List<InventorySlot> itemSlots;

    public override void Initialize()
    {
        //There is an underlying error here that we need to address
        if (UIComponent == null) return;
        UIComponent.gameObject.SetActive(false);
    }
    public override void Start()
    {
        if (UIComponent == null) return;
        InitializeInventory();
    }

    public void InitializeInventory()
    {
        itemSlots = new List<InventorySlot>(InventorySize);
        UIComponent.Initialize(itemSlots);
        for(int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].ParentInventory = this;
        }
        AddItemToInventory(itemToAdd, 1, new Vector2Int(0, 0));
        AddItemToInventory(secondItemToAdd, 1, new Vector2Int(2, 0));


        hoveredSlot = itemSlots[0];
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
   
    public void AddItemToInventory(Item item, int count, Vector2Int at)
    {
        int index = at.y * UIComponent.itemsPerRow + at.x;
        itemSlots[index].Item = item;
        itemSlots[index].SlotIconComponent.sprite = itemSlots[index].Item.ItemSprite;
        itemSlots[index].SlotIconComponent.color = Color.white;
    }

    InventorySlot FindFirstEmptySlot()
    {
        for(int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return itemSlots[i];
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
}
