using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleBeginDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public void OnSingleBeginDrag(PointerEventData eventData, DraggableItem item)
    {
        if (reg.selectedItems.Count <= 1 || !reg.selectedItems.Contains(item))
        {
            reg.ResetItems();
            item.OnBeginDrag(eventData);
        }
    }
}
