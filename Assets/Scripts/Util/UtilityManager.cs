using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    private void Awake()
    {
        InitializeCustomScripts();
    }
    private void Start()
    {
        StartCustomScripts();
    }

    void StartCustomScripts()
    {
        List<Initializable> itemsToInit = Initializable.ItemsToInitialze;
        Debug.Log(itemsToInit.Count);
        for (int i = 0; i < itemsToInit.Count; i++)
        {
            if (itemsToInit[i] == null) Debug.LogWarning("Script is null");
            itemsToInit[i].Start();
        }
    }
    void InitializeCustomScripts()
    {
        List<Initializable> itemsToInit = Initializable.ItemsToInitialze;
        Debug.Log(itemsToInit.Count);
        for(int i = 0; i < itemsToInit.Count; i++)
        {
            if (itemsToInit[i] == null) Debug.LogWarning("Script is null");
            itemsToInit[i].Initialize();
        }
    }
}
