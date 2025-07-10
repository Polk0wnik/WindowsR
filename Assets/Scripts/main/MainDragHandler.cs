using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private List<DraggableItem> selectedItems = new();
    private List<DraggableItem> draggableItems = new();
    private DraggableItem currentDrItem;
    private SelectionFrame frameSelect;
    private Canvas canvas;
    private bool isDraggingAll;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DraggableItem>(false));
        frameSelect = GetComponent<SelectionFrame>();
        canvas = GetComponent<Canvas>();
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
            item.ResetItem();
        selectedItems.Clear();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CheckCurrentDRItem();
        BeginDrag(eventData);
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
        CheckCurrentDRItem();
        ResetSelectedItems();
        currentDrItem.OnPointerClick(eventData);
    }
    private void CheckCurrentDRItem()
    {
        currentDrItem = null;
        foreach(var item in draggableItems)
        {
            if(item.hasHitPointerEnter)
            {
                currentDrItem = item;
                break;
            }
        }
    }
    private void BeginDrag(PointerEventData eventData)
    {
        if(selectedItems.Count <= 1 || !selectedItems.Contains(currentDrItem))
        {
            ResetSelectedItems();
            currentDrItem.OnBeginDrag(eventData);
            currentDrItem.ResetItem();
        }
    }
}
