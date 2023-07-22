using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryUIComponent))]
public class InventoryUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InventoryUIComponent ui = (InventoryUIComponent)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Rebuild UI"))
        {
            ui.DestroyItemSlots();
            ui.SpawnItemSlots();
        }
    }
}
