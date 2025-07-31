using UnityEngine;
using UnityEngine.EventSystems;

public class SingleDrag : MonoBehaviour
{
    RegistrySelectableItems reg;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    public void OnSingleDrag(PointerEventData eventData, DragBase item)
    {
        item?.OnDrag(eventData);
        return;
    }
}
