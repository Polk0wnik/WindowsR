using UnityEngine.EventSystems;
using UnityEngine.UI; 
using UnityEngine;

public class DraggableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline line;
    public bool hasHitPointerEnter { get; private set; }
    public bool InSelectionFrame { get; private set; }
    public bool IsDraggableItem { get; private set; }
    public RectTransform rectTransform { get; set; }
    private Canvas canvas;
    private CanvasGroup canGroup;
    private void Awake()
    {
        line = GetComponent<Outline>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
        LineEnable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit();
        LineDisable();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        PointerEnter();
        LineEnable();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        hasHitPointerEnter = true;
        IsDraggableItem = true;

        canGroup.alpha = 0.5f;
        canGroup.blocksRaycasts = false; 
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        hasHitPointerEnter = true;
        IsDraggableItem = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canGroup.alpha = 1;
        canGroup.blocksRaycasts = true;

        IsDraggableItem = false;
        hasHitPointerEnter = false;

    }
    public void SetInSelectionFrame()
    {
        InSelectionFrame = true;
        LineEnable();
    }
    public void ResetItem()
    {
        InSelectionFrame = false;
        LineDisable();
    }
    public void LineEnable()
    {
        line.enabled = true;
    }
    public void LineDisable()
    {
        if(!InSelectionFrame) 
            line.enabled = false;
    }
    public void PointerEnter()
    { 
        hasHitPointerEnter = true; 
    }
    public void PointerExit()
    { 
        hasHitPointerEnter = false; 
    }
 
}
