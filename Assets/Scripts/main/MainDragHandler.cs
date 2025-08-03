using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler , IPointerUpHandler , IPointerDownHandler
{
    private bool isDraggingAll;
    private ScrollRect scrollRC;

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
    }

    public void OnPointerClick(PointerEventData eventData) // третий
    {
        if(!isDraggingAll)
            reg?.ResetItems();
        reg?.currentDrItem?.OnPointerClick(eventData); 
        isDraggingAll = false;
    }

    public void OnPointerUp(PointerEventData eventData)// второй
    {
        reg?.currentDrItem?.OnPointerUp(eventData);
        if (scrollRC != null)
        {
            scrollRC.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData) // первый 
    {
        reg?.FindCurrentItemDRItem(); 
        reg?.currentDrItem?.OnPointerDown(eventData);
        scrollRC = reg?.currentDrItem?.GetComponentInParent<ScrollRect>();
        if(scrollRC != null)
        {
            scrollRC.enabled = false;
        }
    }
}
