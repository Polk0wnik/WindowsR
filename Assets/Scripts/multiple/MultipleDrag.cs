using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private Canvas can;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
        can = GetComponentInParent<Canvas>();
    }
    public void OnMultipleDrag(PointerEventData eventData)
    {
        foreach(var item in reg.selectedItems)
        {
            if(reg.itemsOffset.ContainsKey(item))
            {
                item.rectTransform.position = GetPosition(eventData,item);
            }
        }
    }
    public Vector2 GetPosition(PointerEventData eventData, DraggableItem item)
    {
        Vector2 newPosition = eventData.position + reg.GetItemOffset(item);
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(can.transform as RectTransform, newPosition, eventData.pressEventCamera,out localPosition);
        return localPosition;
    }
}
