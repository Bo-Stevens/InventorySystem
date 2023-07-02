using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Min(1)] public int MaximumStackAmount = 1;
    public string ItemConsoleIcon;

    public void OnUse()
    {

    }

    public void OnActivate()
    {

    }
}