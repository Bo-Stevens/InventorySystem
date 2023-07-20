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
    
    Vector2Int hoveredSlotPosition;
    InventorySlot selectedSlot;
    InventorySlot hoveredSlot;

    public override void Initialize()
    {
        //There is an underlying error here that we need to address
        if (UIComponent == null) return;
        Debug.Log("Me firts?");
        InitializeInventory();
        UIComponent.gameObject.SetActive(false);
    }
    public override void Start()
    {
        //There is an underlying error here that we need to address
        if (UIComponent == null) return;
        UIComponent.Initialize(ItemSlots);
    }

    public void InitializeInventory()
    {
        ItemSlots = new List<InventorySlot>();
        for(int i = 0; i < InventorySize; i++)
        {
            ItemSlots.Add( new InventorySlot(null, 0));
        }

        ItemSlots[10] = new InventorySlot(itemToAdd, 1);
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
        InventorySlot slotToReturn = new InventorySlot(selectedSlot.Item, selectedSlot.StackAmount);
        selectedSlot.Item = null;
        selectedSlot.StackAmount = 0;
        selectedSlot = null;
        return slotToReturn;
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
    
    void SwapItemsInSlots(InventorySlot from, InventorySlot to)
    {
        Item tempItem = from.Item;
        from.Item = to.Item;
        to.Item = tempItem;
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
}
