using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class DragItemBase : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemContext context { get; private set; }
    public Outline line { get; private set; }
    public RectTransform rectTransform { get; set; }
    private Canvas canvas;
    public CanvasGroup canGroup { get; private set; }
    public Transform acceptParentTrans { get; set; }
    public ItemData currentItemData { get; private set; }
    public Image image { get; private set; }
    public TextMeshProUGUI nameText { get; private set; }
    private RegistrySelectableItems reg;
    private void Awake()
    {
        context = new ItemContext();
        acceptParentTrans = GetComponent<Transform>();

        line = GetComponent<Outline>();
        rectTransform = GetComponent<RectTransform>();
        canGroup = GetComponent<CanvasGroup>();
        image = transform.GetChild(0).GetComponent<Image>();
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        canvas = acceptParentTrans.GetComponentInParent<Canvas>();
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    private void OnEnable()
    {
        reg.AddItemDrag(this);
        reg.AddItemSelectAndDrop(this);
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        context.PointerEnter();
        context.LineEnable(line);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        context.PointerExit();
        context.LineDisable(line);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        context.PointerEnter();
        context.LineEnable(line);
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        context.SetHasHitPointerEnter(true);
        context.SetIsDraggableItem(true);

        context.BlockRaycast(false, 0.5f, canGroup);
        rectTransform.SetAsLastSibling();
        acceptParentTrans = transform.parent;
        rectTransform.SetParent(canvas.transform);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        context.SetHasHitPointerEnter(true);
        context.SetIsDraggableItem(true);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.SetParent(acceptParentTrans);
        context.BlockRaycast(true, 1f, canGroup);
        context.SetHasHitPointerEnter(false);
        context.SetIsDraggableItem(false);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        context.LineDisable(line);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        context.LineEnable(line);
    }
    public void SetDataItem(ItemData itemData)
    {
        currentItemData = itemData;
        currentItemData.SetID();
        image.sprite = itemData.spriteItem;
        nameText.text = itemData.nameItem;
    }
}
