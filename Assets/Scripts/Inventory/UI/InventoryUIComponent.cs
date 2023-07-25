using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIComponent : MonoBehaviour
{
    public int itemsPerRow = 10;
    [SerializeField] Image inventoryPanel;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject itemSlotPanel;
    [SerializeField] Vector2 itemSlotMargin; 
    List<InventorySlot> itemSlots;
    List<GameObject> uiSlots = new List<GameObject>();
    Vector2 itemSlotPrefabScale;

    public void Initialize(List<InventorySlot> itemSlots)
    {
        this.itemSlots = itemSlots;
        itemSlotPrefabScale = itemSlotPanel.transform.parent.GetComponent<RectTransform>().sizeDelta;
        SpawnItemSlots();
    }

    public void SpawnItemSlots()
    {
        GameObject currentRow;
        GameObject itemSlot;
        Vector2 itemSlotSize;
        Vector3 positionOffset;
        Vector2 panelSize = itemSlotPanel.GetComponent<RectTransform>().rect.size;
        int x = 0;
        int y = 0;

        itemSlotSize = (panelSize.x - itemSlotMargin.x * itemsPerRow - itemSlotMargin.x) / (itemsPerRow ) * Vector2.one; 

        currentRow = CreateRow(y);
        positionOffset = new Vector3(itemSlotMargin.x + itemSlotSize.x / 2f, -itemSlotMargin.y - itemSlotSize.y / 2f);
        //This uses Capacity interestingly because of some MonoBehaviour shenanigans. I can't put anything in the List until it's created in this method
        //So I use Capcity instead of Count because Count will always be 0 when this method is run
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            positionOffset = new Vector3((itemSlotSize.x + itemSlotMargin.x) * x + itemSlotMargin.x + itemSlotSize.x / 2f, positionOffset.y, 0);
            if(x >= itemsPerRow)
            {
                x = 0;
                y += 1;
                positionOffset = new Vector3(itemSlotMargin.x + itemSlotSize.x / 2f, (-itemSlotSize.y - itemSlotMargin.y) * y - itemSlotMargin.y - itemSlotSize.y / 2f);

                currentRow = CreateRow(y);
            }

            itemSlot = Instantiate(itemSlotPrefab, currentRow.transform);
            itemSlot.GetComponent<RectTransform>().sizeDelta = Vector2.one * itemSlotSize;
            itemSlot.GetComponent<RectTransform>().localPosition = Vector3.zero + positionOffset;
            itemSlots.Add(itemSlot.GetComponent<InventorySlot>());
            uiSlots.Add(itemSlot);
            x += 1;
        }

        
        float newPanelHeight = (itemSlotSize.y + itemSlotMargin.y) * (y + 1) + itemSlotMargin.y;
        //This part exists for some extra fuckery to handle the fact that there are borders around the UI panels
        Debug.Log("Height diff " + (itemSlotPanel.transform.parent.GetComponent<RectTransform>().rect.height - itemSlotPanel.GetComponent<RectTransform>().rect.height));
        newPanelHeight += itemSlotPanel.transform.parent.GetComponent<RectTransform>().rect.height - itemSlotPanel.GetComponent<RectTransform>().rect.height;

        itemSlotPanel.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(itemSlotPanel.transform.parent.GetComponent<RectTransform>().sizeDelta.x, newPanelHeight);

    }

    GameObject CreateRow(int rowNumber)
    {
        GameObject currentRow = new GameObject("Row " + rowNumber);
        currentRow.transform.parent = itemSlotPanel.transform;
        currentRow.transform.localPosition = Vector3.zero;
        currentRow.transform.localScale = Vector3.one;
        uiSlots.Add(currentRow);
        return currentRow;
    }

    public void DestroyItemSlots()
    {
        for (int i = 0; i < uiSlots.Count; i++)
        {
            Destroy(uiSlots[i]);
        }
        itemSlots = new List<InventorySlot>(itemSlots.Count);
    }

}
