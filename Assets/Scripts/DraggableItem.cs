using UnityEngine.EventSystems;
using UnityEngine.UI; 
using UnityEngine;

public class DraggableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline line;
    public bool isPointerEnter { get; private set; }
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
        isPointerEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        line.enabled = false;
        isPointerEnter = false;
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
