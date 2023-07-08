using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIComponent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    

    public void SetText(string newString)
    {
        text.text = newString;
    }
}
