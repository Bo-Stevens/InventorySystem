using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DebugManager))]
public class DebugManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DebugManager debugManager = (DebugManager)target;
        DrawDefaultInspector();
        if(GUILayout.Button("Enable/Disable Debug Lines"))
        {
            debugManager.ToggleActivationState();
        }
    }
}
