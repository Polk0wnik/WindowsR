using UnityEngine.EventSystems;
using UnityEngine.UI; 
using UnityEngine;

public class DraggableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline line;
    public bool hasHitPointerEnter { get; private set; }
    public bool InSelectionFrame { get; private set; }
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
        line.enabled = true;
        hasHitPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        line.enabled = false;
        hasHitPointerEnter = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canGroup.alpha = 0.5f;
        canGroup.blocksRaycasts = false;
        hasHitPointerEnter = true;
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        hasHitPointerEnter = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canGroup.alpha = 1;
        hasHitPointerEnter = false;
        canGroup.blocksRaycasts = true;
    }
    public void SetInSelectionFrame()
    {
        InSelectionFrame = true;
        line.enabled = true;
    }
    public void ResetInSelectionFrame()
    {
        InSelectionFrame = false;
        line.enabled = false;
    }

}
