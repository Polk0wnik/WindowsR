using System.Collections.Generic;
using UnityEngine;

public class RegistrySelectableItems : MonoBehaviour
{
    public readonly List<DraggableItem> selectedItems = new();
    public readonly List<DraggableItem> draggableItems = new();
    public readonly Dictionary<DraggableItem, Vector2> itemsOffset = new();
    public DraggableItem currentDrItem { get; private set; }
    private SelectionFrame frameSelect;
    private void Awake()
    {
        draggableItems.AddRange(GetComponentsInChildren<DraggableItem>(false));
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
    public void AddItem(DraggableItem item)
    {
        selectedItems.Add(item);
    }
    public void ResetItems()
    {
        foreach (var item in selectedItems)
            item.ResetItem();
        selectedItems.Clear();
    }
    public void SetCurrentItem()
    {
        currentDrItem = null;
        foreach (var item in draggableItems)
        {
            if (item.hasHitPointerEnter)
            {
                currentDrItem = item;
                break;
            }
        }
    }
    public void SetItemOffset(DraggableItem item, Vector2 offset)
    {
        itemsOffset[item] = offset;
    }
    public void ResetItemOffset()
    {
        itemsOffset.Clear();
    }
    public Vector2 GetItemOffset(DraggableItem item)
    {
        return itemsOffset[item];
    }
}