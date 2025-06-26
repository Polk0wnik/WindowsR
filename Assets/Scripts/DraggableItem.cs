using UnityEngine.EventSystems;
using UnityEngine.UI; 
using UnityEngine;

public class DraggableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Outline line;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        line.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canGroup.alpha = 0.5f;
        canGroup.blocksRaycasts = false;
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canGroup.alpha = 1;
        canGroup.blocksRaycasts = true;
    }
}
