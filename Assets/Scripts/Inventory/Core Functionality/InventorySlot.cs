using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public Item Item;
    public int StackAmount;
    public InventorySlot(Item Item, int StackAmount)
    {
        this.Item = Item;
        this.StackAmount = StackAmount;
    }
}
