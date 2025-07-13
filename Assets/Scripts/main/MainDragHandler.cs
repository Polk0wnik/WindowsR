using System.Collections.Generic;
using Unity.VisualScripting;
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
        mbd = GetComponent<MultipleBeginDrag>();
        md = GetComponent<MultipleDrag>();
        med = GetComponent<MultipleEndDrag>();
        sbd = GetComponent<SingleBeginDrag>();
        sd = GetComponent<SingleDrag>();
        sed = GetComponent<SingleEndDrag>();
        reg = GetComponent<RegistrySelectableItems>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        reg.SetCurrentItem();
        sbd.OnSingleBeginDrag(eventData, reg.currentDrItem);
        isDraggingAll = mbd.OnMultipleBeginDrag(eventData, reg.currentDrItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDraggingAll)
        {
            md.OnMultipleDrag(eventData);
        }
        sd.OnSingleDrag(eventData, reg.currentDrItem);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        med.OnMultipleEndDrag(eventData);
        sed.OnSingleEndDrag(eventData, reg.currentDrItem);
        isDraggingAll = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        reg.ResetItems();
        reg.SetCurrentItem();
        reg.currentDrItem.OnPointerClick(eventData);
    }

}
