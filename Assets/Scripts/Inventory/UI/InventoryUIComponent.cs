using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIComponent : MonoBehaviour
{
    [SerializeField] int itemsPerRow = 10;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image inventoryPanel;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject itemSlotPanel;
    [SerializeField] Vector2 itemSlotMargin; 
    List<Image> inventorySlots;
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
        positionOffset = new Vector3(itemSlotMargin.x, -itemSlotMargin.y);
        //This uses Capacity interestingly because of some MonoBehaviour shenanigans. I can't put anything in the List until it's created in this method
        //So I use Capcity instead of Count because Count will always be 0 when this method is run
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            positionOffset = new Vector3((itemSlotSize.x + itemSlotMargin.x) * x + itemSlotMargin.x, positionOffset.y, 0);
            if(x >= itemsPerRow)
            {
                x = 0;
                y += 1;
                positionOffset = new Vector3(itemSlotMargin.x, (-itemSlotSize.y - itemSlotMargin.y) * y - itemSlotMargin.y);
                if (positionOffset.y - itemSlotSize.y < -panelSize.y)
                {
                    Debug.LogWarning("Cannot fit an inventory of size " + itemSlots.Count + " in this inventory while using square slot tiles");
                    y -= 1;
                    break;
                }
                currentRow = CreateRow(y);
            }

            itemSlot = Instantiate(itemSlotPrefab, currentRow.transform);
            itemSlot.GetComponent<RectTransform>().sizeDelta = Vector2.one * itemSlotSize;
            itemSlot.GetComponent<RectTransform>().localPosition = Vector3.zero + positionOffset;
            itemSlots.Add(itemSlot.GetComponent<InventorySlot>());
            uiSlots.Add(itemSlot);
            x += 1;
        }

        float hangingSpace = panelSize.y - ((itemSlotSize.y + itemSlotMargin.y) * (y + 1)) - itemSlotMargin.y;
        itemSlotPanel.transform.parent.GetComponent<RectTransform>().sizeDelta = itemSlotPrefabScale - new Vector2(0, hangingSpace);
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
        for(int i = 0; i < uiSlots.Count; i++)
        {
            Destroy(uiSlots[i]);
        }
    }
    public void SetText(string newString)
    {
        text.text = newString;
    }
}
