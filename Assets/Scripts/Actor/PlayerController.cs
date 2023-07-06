using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inventory Inventory;

    void Start()
    {
        InputManager.InputActionSet.Combat.Inventory.performed += OpenInventory;
        InputManager.InputActionSet.Combat.Interact.performed += Interact;
        InputManager.InputActionSet.Inventory.Escape.performed += CloseInventory;
        Inventory.PrintInventoryContentsToConsole();
    }
    
    void OpenInventory(InputAction.CallbackContext context)
    {
        InputManager.ChangeActionMap(InputManager.InputActionSet.Inventory);
        Inventory.PrintInventoryContentsToConsole();
    }
    void CloseInventory(InputAction.CallbackContext context)
    {
        Debug.Log("Closing Inventory");
        InputManager.ChangeActionMap(InputManager.InputActionSet.Combat);
    }

    void Interact(InputAction.CallbackContext context)
    {

    }
}
