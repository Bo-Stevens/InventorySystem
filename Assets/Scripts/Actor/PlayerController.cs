using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public List<InteractableObject> InteractablesInRange;
    public Inventory Inventory;
    Container openContainer;

    private void OnEnable()
    {
        Instance = this;
    }
    void Start()
    {
        InputManager.InputActionSet.Combat.Inventory.performed += OpenInventory;
        InputManager.InputActionSet.Combat.Interact.performed += Interact;
        InputManager.InputActionSet.Inventory.Escape.performed += CloseInventory;
        InputManager.InputActionSet.Inventory.TakeItem.performed += TakeItemFromInventory;
    }
    private void Update()
    {

    }

    public IEnumerator Cycle()
    {
        while(InteractablesInRange.Count > 0 && DebugManager.Instance.DebugLinesActive)
        {
            yield return null;
            DebugManager.Instance.HighlightClosestDebugCircle();
        }
        yield return null;
        DebugManager.Instance.HighlightClosestDebugCircle();
    }
    void OpenInventory(InputAction.CallbackContext context)
    {
        Inventory.OpenInventory();
    }
    void CloseInventory(InputAction.CallbackContext context)
    {
        if (openContainer != null) { openContainer.Interact(); openContainer = null; }
        else Inventory.CloseInventory();
    }
    void Interact(InputAction.CallbackContext context)
    {
        if (InteractablesInRange.Count == 0) return;
        InteractableObject closestObject = DebugManager.Instance.FindClosestOverlappingInteractable(InteractablesInRange);
        if (closestObject is Container) openContainer = (Container)closestObject;
        closestObject.Interact();
    }

    void TakeItemFromInventory(InputAction.CallbackContext context)
    {
        if (openContainer == null) return;
        InventorySlot itemSlot = openContainer.Inventory.TakeSelectedItemFromInventory();
        Inventory.AddItemToInventory(itemSlot.Item, itemSlot.StackAmount);
    }
}
