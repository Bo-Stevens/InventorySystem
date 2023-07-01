using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDebugManager : MonoBehaviour
{
    public static InventoryDebugManager Instance;
    public Sprite DebugCircleSprite;
    private void Awake()
    {
        Instance = this;
    }
}
