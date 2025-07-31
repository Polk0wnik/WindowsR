using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private Canvas can;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
        can = GetComponentInParent<Canvas>();
    }
    public void OnMultipleDrag(PointerEventData eventData)
    {
        Vector2 currentCursorLocalPointOnCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            can.transform as RectTransform, eventData.position, eventData.pressEventCamera, out currentCursorLocalPointOnCanvas);
        foreach (var item in reg.selectedItems)
        {
            if (!reg.itemsOffset.ContainsKey(item)) continue;
            item.rectTransform.anchoredPosition = currentCursorLocalPointOnCanvas + reg.GetItemOffset(item);
            //Не надо вызывать функцую Drag, тк мы ее выполняем самостоятельно(по другому)
        }
    }
}
