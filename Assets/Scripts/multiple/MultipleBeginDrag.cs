using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleBeginDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public bool OnMultipleBeginDrag(PointerEventData eventData, DraggableItem item)
    {
        if (reg.selectedItems.Count > 1 && reg.selectedItems.Contains(item))
        {
            reg.ResetItemOffset();
            CalculateOffset(eventData);
            return true;
        }
        else return false;
    }
    private void CalculateOffset(PointerEventData eventData)
    {
        foreach(var item in reg.selectedItems)
        {
            Vector2 offset = (Vector2)item.rectTransform.position - eventData.position;
            reg.SetItemOffset(item, offset);
            item.GetComponent<CanvasGroup>().blocksRaycasts = false;
            item.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
    }
}
