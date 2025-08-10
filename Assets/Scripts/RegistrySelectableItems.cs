using System.Collections.Generic;
using System.Linq;
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
        frameSelect.onAddSelectedItem += AddItemSelectAndDrop;
        frameSelect.onResetSelectedItem += ResetItems;
    }
    private void OnDisable()
    {
        frameSelect.onAddSelectedItem -= AddItemSelectAndDrop;
        frameSelect.onResetSelectedItem -= ResetItems;
    }
    public void ResetItems()
    {
        foreach (var item in selectedItems)
            item?.context.ResetInFrame(item.line);
        selectedItems?.Clear(); 
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
    public void ResetDropItems()
    {
        foreach(var item in dropItems)
        {
            item?.context.ResetInFrame(item.line);
        }
        dropItems?.Clear();
    }
    public void SetItemOffset(DragBase item, Vector2 offset)
    {
        itemsOffset[item] = offset;
    }
    public void ResetItemOffset()
    {
        itemsOffset?.Clear();
    }
    public Vector2 GetItemOffset(DragBase item)
    {
        return itemsOffset[item];
    }
    public void AddItemSelectAndDrop(DragBase item)
    {

        if (item.gameObject.layer == 6 && !selectedItems.Contains(item))
        {
            selectedItems.Add(item);
            dropItems.Add(item);
        }
    }
    public void AddItemDrag(DragBase item)
    {

        if (!draggableItems.Contains(item))
        {
            draggableItems.Add(item);
        }
    }
}