using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleEndDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public void OnMultipleEndDrag(PointerEventData eventData)
    { 
        foreach (var item in reg.selectedItems)
        {
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
            item.GetComponent<CanvasGroup>().alpha = 1f;
        }
        reg?.ResetItemOffset();
    }
}
