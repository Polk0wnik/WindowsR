using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleBeginDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private Canvas can;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
        can = GetComponentInParent<Canvas>();
    }
    public bool OnMultipleBeginDrag(PointerEventData eventData, DragItemBase item)
    {
        if (reg.selectedItems.Count > 1 && reg.selectedItems.Contains(item))
        {
            reg?.ResetItemOffset();
            CalculateOffset(eventData);   
            return true;
        }
        else return false;
    }
    private void CalculateOffset(PointerEventData eventData)
    {
        Vector2 cursorLocalPointOnCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            can.transform as RectTransform, eventData.position, eventData.pressEventCamera, out cursorLocalPointOnCanvas);
        foreach (var item in reg.selectedItems)
        {
            item.OnBeginDrag(eventData);
            Vector2 offset = (Vector2)item.rectTransform.anchoredPosition - cursorLocalPointOnCanvas;
            reg?.SetItemOffset(item, offset);

        }
    }
}
