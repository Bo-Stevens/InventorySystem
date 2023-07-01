using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovementComponent MovementComponent;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.InputActionSet.Combat.Inventory.performed += OpenInventory;
        InputManager.Instance.InputActionSet.Combat.Interact.performed += Interact;
    }
    
    void OpenInventory(InputAction.CallbackContext context)
    {

    }

    void Interact(InputAction.CallbackContext context)
    {

    }
}
