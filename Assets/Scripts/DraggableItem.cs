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
    public Transform acceptParentTrans { get; set; }
    private void Awake()
    {
        line = GetComponent<Outline>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canGroup = GetComponent<CanvasGroup>();
        acceptParentTrans = GetComponent<Transform>();
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

        BlockRaycast(false, 0.5f);
        rectTransform.SetAsLastSibling();
        acceptParentTrans = transform.parent;
        rectTransform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        hasHitPointerEnter = true;
        IsDraggableItem = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.SetParent(acceptParentTrans);
        BlockRaycast(true, 1f);

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
    public void BlockRaycast(bool isBlockRay, float alpha)
    {
        canGroup.alpha = alpha;
        canGroup.blocksRaycasts = isBlockRay;
    }
 
}
