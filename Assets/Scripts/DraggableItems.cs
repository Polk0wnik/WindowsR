using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private List<DraggableItem> selectedItems = new();
    private List<DraggableItem> draggableItems = new();
    private DraggableItem currentDrItem;
    private bool isDraggingAll;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DraggableItem>(false));
        Debug.Log(draggableItems.Count);
    }
    private void AddItemInSelectedList(DraggableItem item)
    {
        selectedItems.Add(item);
        Debug.Log("add new item " + item);
    }
    private void ClearSelectedItems()
    {
        selectedItems.Clear();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selectedItems.Count == 0)
        {
            foreach (DraggableItem item in draggableItems)
            {
                if (item.isPointerEnter)
                {
                    currentDrItem = item;
                    currentDrItem.OnBeginDrag(eventData);
                }
            }
                return;
        }
        isDraggingAll = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggingAll)
        {
            currentDrItem.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentDrItem.OnEndDrag(eventData);
        isDraggingAll = false;
    }
}
