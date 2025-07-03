using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private List<DraggableItem> selectedItems = new();
    private List<DraggableItem> draggableItems = new();
    private DraggableItem currentDrItem;
    private SelectableRegistry registry;
    private SelectionFrameFromScreen frameSelect;
    private bool isDraggingAll;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DraggableItem>(false));
        registry = GetComponent<SelectableRegistry>();
        frameSelect = GetComponent<SelectionFrameFromScreen>();
    }
    private void OnEnable()
    {
        frameSelect.onAddSelectedItem += AddItemInSelectedList;
        frameSelect.onResetSelectedItem += ResetSelectedItems;
    }
    private void OnDisable()
    {
        frameSelect.onAddSelectedItem -= AddItemInSelectedList;
        frameSelect.onResetSelectedItem -= ResetSelectedItems;
    }
    private void AddItemInSelectedList(DraggableItem item)
    {
        selectedItems.Add(item);
        Debug.Log("add new item " + item);
    }
    private void ResetSelectedItems()
    {
        foreach (var item in selectedItems)
            item.ResetInSelectionFrame();
        selectedItems.Clear();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selectedItems.Count == 0)
        {
            foreach (DraggableItem item in draggableItems)
            {
                if (item.hasHitPointerEnter)
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

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
