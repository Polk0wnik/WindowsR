using UnityEngine;
using UnityEngine.EventSystems;

public class MainDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private bool isDraggingAll;

    MultipleBeginDrag mbd;
    MultipleDrag md;
    MultipleEndDrag med;

    SingleBeginDrag sbd;
    SingleDrag sd;
    SingleEndDrag sed;

    RegistrySelectableItems reg;

    private void Awake()
    {
        reg = GetComponent<RegistrySelectableItems>();
         
        mbd = GetComponent<MultipleBeginDrag>();
        md = GetComponent<MultipleDrag>();
        med = GetComponent<MultipleEndDrag>();

        sbd = GetComponent<SingleBeginDrag>();
        sd = GetComponent<SingleDrag>();
        sed = GetComponent<SingleEndDrag>(); 
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        reg?.FindCurrentItemDRItem();
        sbd?.OnSingleBeginDrag(eventData, reg.currentDrItem);
        isDraggingAll = mbd.OnMultipleBeginDrag(eventData, reg.currentDrItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDraggingAll)
            sd?.OnSingleDrag(eventData, reg.currentDrItem);
        md?.OnMultipleDrag(eventData); 
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        med?.OnMultipleEndDrag(eventData);
        sed?.OnSingleEndDrag(eventData, reg.currentDrItem);
        isDraggingAll = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isDraggingAll)
            reg?.ResetItems();
        reg?.FindCurrentItemDRItem();
        reg?.currentDrItem?.OnPointerClick(eventData);
    }

}
