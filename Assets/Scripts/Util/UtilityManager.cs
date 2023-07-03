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
        for(int i = 0; i < itemsToInit.Count; i++)
        {
            itemsToInit[i].Initialize();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
