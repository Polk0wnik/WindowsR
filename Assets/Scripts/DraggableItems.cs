using System.Collections.Generic;
using UnityEngine;

public class DraggableItems : MonoBehaviour
{
    private List<DraggableItem> selectedItems = new();
    private List<DraggableItem> draggableItems = new();
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
}
