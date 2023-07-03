using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Inventory : Initializable
{
    [HideInInspector] public InventorySlot[][] ItemSlots;
    [HideInInspector] public InventorySlot activeSlot;
    public Vector2Int InventoryDimensions;
    [SerializeField] Item itemToAdd;
    [SerializeField] Item nullItem;

    public override void Initialize()
    {
        InitializeInventory();
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
            }
        }
        ItemSlots[2][3] = new InventorySlot(itemToAdd);
    }

    public void PrintInventoryContentsToConsole()
    {
        string rowContents = "";
        string fullInventory = "";
        for(int x = 0; x < ItemSlots.Length; x++)
        {
            for(int y = 0; y < ItemSlots[x].Length; y++)
            {
                rowContents += "[" + ItemSlots[x][y].Item.ItemConsoleIcon + "] ";
            }
            fullInventory += rowContents + "\n";
            rowContents = "";
        }
        Debug.Log(fullInventory);
    }
}
