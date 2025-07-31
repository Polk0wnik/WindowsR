using System.Collections.Generic;
using UnityEngine;

public class RegistrySelectableItems : MonoBehaviour
{
    public  List<DragBase> selectedItems = new();
    public  List<DragBase> dropItems = new();
    public  List<DragBase> draggableItems = new();
    public readonly Dictionary<DragBase, Vector2> itemsOffset = new();
    public DragBase currentDrItem { get; private set; }
    private SelectionFrame frameSelect;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DragBase>(false));
        frameSelect = GetComponent<SelectionFrame>();
    }
    private void OnEnable()
    {
        frameSelect.onAddSelectedItem += AddItem;
        frameSelect.onResetSelectedItem += ResetItems;
    }
    private void OnDisable()
    {
        frameSelect.onAddSelectedItem -= AddItem;
        frameSelect.onResetSelectedItem -= ResetItems;
    }
    public void AddItem(DragBase item)
    {
        SetItem(item);
    }
    public void ResetItems()
    {
        foreach (var item in selectedItems)
            item?.context.ResetInFrame(item.line);
        selectedItems.Clear(); 
    }
    public void FindCurrentItemDRItem()
    {
        currentDrItem = null;
        foreach (var item in draggableItems)
        {
            if (item.context.HasHitPointerEnter)
            {
                currentDrItem = item;
                break;
            }
        }
    }
    public void SetItemOffset(DragBase item, Vector2 offset)
    {
        itemsOffset[item] = offset;
    }
    public void ResetItemOffset()
    {
        itemsOffset.Clear();
    }
    public Vector2 GetItemOffset(DragBase item)
    {
        return itemsOffset[item];
    }
    private void SetItem(DragBase drBase)
    {
        if(drBase.gameObject.layer == 6)
        {
            selectedItems.Add(drBase);
            dropItems.Add(drBase);
        }
    }
}