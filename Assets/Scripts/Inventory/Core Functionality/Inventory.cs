using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory : Initializable
{
    [HideInInspector] public InventorySlot[][] ItemSlots;
    [HideInInspector] public InventorySlot hoveredSlot;
    public Vector2Int InventoryDimensions;
    [SerializeField] Item itemToAdd;
    [SerializeField] Item nullItem;
    [SerializeField] InventoryUIComponent uiComponent;
    
    Vector2Int hoveredSlotPosition;
    InventorySlot selectedSlot;

    public override void Initialize()
    {
        //There is an underlying error here that we need to address
        if (uiComponent == null) return;
        InitializeInventory();
        uiComponent.gameObject.SetActive(false);
    }

    public void InitializeInventory()
    {
        ItemSlots = new InventorySlot[InventoryDimensions.x][];
        for (int x = 0; x < ItemSlots.Length; x++)
        {
            ItemSlots[x] = new InventorySlot[InventoryDimensions.y];
            for(int y = 0; y < ItemSlots[x].Length; y++)
            {
                ItemSlots[x][y] = new InventorySlot(nullItem);
                if (ItemSlots[x][y] == null) Debug.Log("Created null item");
            }
        }
        ItemSlots[2][3] = new InventorySlot(itemToAdd);
        hoveredSlot = ItemSlots[0][0];
        hoveredSlotPosition = Vector2Int.zero;
    }

    public void OpenInventory()
    {
        InputManager.ChangeActionMap(InputManager.InputActionSet.Inventory);
        uiComponent.gameObject.SetActive(true);
        BindKeys();
        RefreshInventoryText();
    }
    
    public void CloseInventory()
    {
        InputManager.ChangeActionMap(InputManager.InputActionSet.Combat);
        UnbindKeys();
        uiComponent.gameObject.SetActive(false);
    }
    
    public void RefreshInventoryText()
    {
        string rowContents = "";
        string fullInventory = "";
        for(int x = 0; x < ItemSlots.Length; x++)
        {
            for(int y = 0; y < ItemSlots[x].Length; y++)
            {
                if(ItemSlots[x][y] == selectedSlot) rowContents += "[<color=red>" + ItemSlots[x][y].Item.ItemConsoleIcon + "</color>]";
                else if(ItemSlots[x][y] == hoveredSlot) rowContents += "[<color=green>" + ItemSlots[x][y].Item.ItemConsoleIcon + "</color>]";
                else rowContents += "[" + ItemSlots[x][y].Item.ItemConsoleIcon + "]";
            }
            fullInventory += rowContents;
            rowContents = "";
        }
        uiComponent.SetText(fullInventory);
    }

    public void AddItemToInventory(Item itemToAdd)
    {

    }

    void BindKeys()
    {
        if (uiComponent == null) return;
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
        hoveredSlot = ItemSlots[hoveredSlotPosition.y][hoveredSlotPosition.x];
        RefreshInventoryText();
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
        RefreshInventoryText();
    }

    public override void Start()
    {

    }
}
