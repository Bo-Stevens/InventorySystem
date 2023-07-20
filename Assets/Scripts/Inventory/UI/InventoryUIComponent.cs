using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIComponent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image inventoryPanel;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject itemSlotPanel;
    [SerializeField] Vector2 itemSlotMargin; 
    List<Image> inventorySlots;
    List<InventorySlot> itemSlots;

    public void Initialize(List<InventorySlot> itemSlots)
    {
        this.itemSlots = itemSlots;
        SpawnItemSlots();

    }

    void SpawnItemSlots()
    {
        GameObject currentRow;
        GameObject itemSlot;
        int numItems = itemSlots.Count;
        Vector2 itemSlotSize = itemSlotPrefab.GetComponent<Image>().sprite.bounds.size * itemSlotPrefab.GetComponent<RectTransform>().rect.size;
        Vector2 panelSize = inventoryPanel.GetComponent<RectTransform>().rect.size;
        float panelArea = panelSize.x * panelSize.y;
        int itemSlotArea = Mathf.RoundToInt(Mathf.Sqrt(panelArea / numItems));

        
        float rowSize = itemSlotSize.y;
        float columnSize = itemSlotSize.x;

        for (int y = 0; y < 8; y++)
        {
            currentRow = new GameObject("Row " + y);
            currentRow.transform.parent = itemSlotPanel.transform;
            currentRow.transform.localPosition = Vector3.zero;
            currentRow.transform.localScale = Vector3.one;
            for (int x = 0; x < 12; x++)
            {
                itemSlot = Instantiate(itemSlotPrefab, currentRow.transform);
                itemSlot.GetComponent<RectTransform>().localPosition = Vector3.zero + new Vector3((columnSize + itemSlotMargin.x) * x, (-rowSize - itemSlotMargin.y) * y, 0);
                itemSlot.GetComponent<RectTransform>().sizeDelta = Vector2.one * itemSlotArea;
            }
        }
    }
    public void SetText(string newString)
    {
        text.text = newString;
    }
}
