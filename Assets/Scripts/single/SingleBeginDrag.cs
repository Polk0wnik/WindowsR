using UnityEngine;
using UnityEngine.EventSystems;

public class SingleBeginDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public void OnSingleBeginDrag(PointerEventData eventData, DragBase item)
    {
        if (reg.selectedItems.Count <= 1 || !reg.selectedItems.Contains(item))
        {
            item?.OnBeginDrag(eventData);
            reg.ResetItems();
        }
    }
}
