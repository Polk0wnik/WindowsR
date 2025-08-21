using UnityEngine;
using UnityEngine.EventSystems;

public class SingleEndDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public void OnSingleEndDrag(PointerEventData eventData, DragItemBase item)
    {
        item?.OnEndDrag(eventData);  
        reg.ResetDropItems();
    }
}
