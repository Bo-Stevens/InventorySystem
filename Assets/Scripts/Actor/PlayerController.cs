using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inventory Inventory;

    void Start()
    {
        InputManager.Instance.InputActionSet.Combat.Inventory.performed += OpenInventory;
        InputManager.Instance.InputActionSet.Combat.Interact.performed += Interact;
        Inventory.PrintInventoryContentsToConsole();
    }
    
    void OpenInventory(InputAction.CallbackContext context)
    {
        Inventory.PrintInventoryContentsToConsole();
    }

    void Interact(InputAction.CallbackContext context)
    {

    }
}
