using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [HideInInspector] public Inventory ParentInventory;
    public Item Item;
    public Image SlotIconComponent;
    public int StackAmount;

    Vector3 startPosition;
    Vector3 startScale;

    private void Start()
    {
        startPosition = SlotIconComponent.transform.position;
        startScale = transform.localScale;
        gameObject.layer = 5;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SlotIconComponent.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SlotIconComponent.transform.localPosition = Vector3.zero;
        SlotIconComponent.transform.localScale = startScale;
        SlotIconComponent.GetComponent<Canvas>().sortingOrder = 1;
        GameObject other = eventData.pointerCurrentRaycast.gameObject;

        if (other == null) return;

        InventorySlot dropInventorySlot = other.GetComponent<InventorySlot>();

        if (dropInventorySlot == null) return;
        
        ParentInventory.SwapItemsInSlots(this, dropInventorySlot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SlotIconComponent.transform.localScale *= 1.25f;
        SlotIconComponent.GetComponent<Canvas>().sortingOrder = 2;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on item " + Item.name);
    }
}
