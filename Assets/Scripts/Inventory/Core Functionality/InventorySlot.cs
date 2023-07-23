using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Item Item;
    public int StackAmount;
    public Image SlotIconComponent;

    private void Awake()
    {
        Item = null;
        StackAmount = 1;
    }
}
