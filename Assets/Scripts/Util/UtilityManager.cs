using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
    private void Awake()
    {
        InitializeCustomScripts();
    }


    void InitializeCustomScripts()
    {
        List<Initializable> itemsToInit = Initializable.ItemsToInitialze;
        Debug.Log(itemsToInit.Count);
        for(int i = 0; i < itemsToInit.Count; i++)
        {
            itemsToInit[i].Initialize();
        }
    }
}
