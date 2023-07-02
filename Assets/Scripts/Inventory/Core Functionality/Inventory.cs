using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    [HideInInspector] public InventorySlot[][] ItemSlots;
    [HideInInspector] public InventorySlot activeSlot;
    public Vector2Int InventoryDimensions;

    public void InitializeInventory()
    {
        ItemSlots = new InventorySlot[InventoryDimensions.x][];
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i] = new InventorySlot[InventoryDimensions.y];
        }
    }
}
