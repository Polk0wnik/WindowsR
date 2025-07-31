using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DragBase : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public ItemContext context { get; private set; }
    public Outline line { get; private set; }
    public RectTransform rectTransform { get; set; }
    private Canvas canvas;
    public CanvasGroup canGroup { get; private set; }
    public Transform acceptParentTrans { get; set; }
    private void Awake()
    {
        context = new ItemContext();
        acceptParentTrans = GetComponent<Transform>();
        canvas = acceptParentTrans.GetComponentInParent<Canvas>();

        line = GetComponent<Outline>();
        rectTransform = GetComponent<RectTransform>();
        canGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        context.PointerEnter();
        context.LineEnable(line);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        context.PointerExit();
        context.LineDisable(line);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        context.PointerEnter();
        context.LineEnable(line);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        context.SetHasHitPointerEnter(true);
        context.SetIsDraggableItem(true);

        context.BlockRaycast(false, 0.5f, canGroup);
        rectTransform.SetAsLastSibling();
        acceptParentTrans = transform.parent;
        rectTransform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        context.SetHasHitPointerEnter(true);
        context.SetIsDraggableItem(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.SetParent(acceptParentTrans);
        context.BlockRaycast(true, 1f, canGroup);
        context.SetHasHitPointerEnter(false);
        context.SetIsDraggableItem(false);

    }

}
