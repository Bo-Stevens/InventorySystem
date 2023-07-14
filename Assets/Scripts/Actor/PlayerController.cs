using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public List<InteractableObject> InteractablesInRange;
    public Inventory Inventory;

    private void OnEnable()
    {
        Instance = this;
    }
    void Start()
    {
        InputManager.InputActionSet.Combat.Inventory.performed += OpenInventory;
        InputManager.InputActionSet.Combat.Interact.performed += Interact;
        InputManager.InputActionSet.Inventory.Escape.performed += CloseInventory;
        Inventory.RefreshInventoryText();
    }
    private void Update()
    {

    }
    public IEnumerator Cycle()
    {
        while(InteractablesInRange.Count > 0 && DebugManager.Instance.DebugLinesActive)
        {
            yield return null;
            DebugManager.Instance.HighlightClosestDebugCircle(transform.position);
        }
        yield return null;
        DebugManager.Instance.HighlightClosestDebugCircle(transform.position);
    }
    void OpenInventory(InputAction.CallbackContext context)
    {
        Inventory.OpenInventory();
    }
    void CloseInventory(InputAction.CallbackContext context)
    {
        Inventory.CloseOpenInventory();
    }
    void Interact(InputAction.CallbackContext context)
    {
        if (InteractablesInRange.Count == 0) return;
        InteractableObject closestObject = DebugManager.Instance.FindClosestOverlappingInteractable(InteractablesInRange);
        closestObject.Interact();
    }

}
