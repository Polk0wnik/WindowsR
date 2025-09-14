using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegistrySelectableItems : MonoBehaviour
{
    public  List<DragItemBase> selectedItems = new();
    public  List<DragItemBase> dropItems = new();
    public  List<DragItemBase> draggableItems = new();
    public readonly Dictionary<DragItemBase, Vector2> itemsOffset = new();
    public readonly Dictionary<string ,DragItemBase> windowFromGridID = new();
    public readonly Dictionary<string ,DragItemBase> windowsID = new();
    public readonly Dictionary<string ,DragItemBase> itemsID = new();
    public DragItemBase currentDrItem { get; private set; }
    private SelectionFrame frameSelect;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DragItemBase>(false));
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
    public void SetItemOffset(DragItemBase item, Vector2 offset)
    {
        itemsOffset[item] = offset;
    }
    public void ResetItemOffset()
    {
        itemsOffset?.Clear();
    }
    public Vector2 GetItemOffset(DragItemBase item)
    {
        return itemsOffset[item];
    }
    public void AddItemSelectAndDrop(DragItemBase item)
    {

        if (item.currentItemData.GetType() == typeof(ItemData) && !selectedItems.Contains(item))
        {
            selectedItems.Add(item);
            dropItems.Add(item);
        }
    }
    public void AddItemDrag(DragItemBase item)
    {

        if (!draggableItems.Contains(item))
        {
            draggableItems.Add(item);
        }
    }
    public void AddItem(DragItemBase item, string id)
    {
        if (!itemsID.ContainsKey(id))
        {
            itemsID.Add(id, item);
        }
        AddItemSelectAndDrop(item);
        AddItemDrag(item);
    }
    public void RemoveItem(string id)
    {
        if (itemsID.TryGetValue(id, out DragItemBase item))
        {
            itemsID.Remove(id);
            draggableItems.Remove(item);
            dropItems.Remove(item);
            Destroy(item.gameObject);
        }
    }
    public void AddWindow(DragItemBase item, string id)
    {
        if (!windowsID.ContainsKey(id))
        {
            AddItemDrag(item);
            windowsID.Add(id, item);
        }
    }
    public void AddMiniWindow(DragItemBase item, string id)
    {
        if (!windowFromGridID.ContainsKey(id))
        {
            AddItemDrag(item);
            windowFromGridID.Add(id, item);
        }
    }
}