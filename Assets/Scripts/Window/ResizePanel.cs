using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ResizePanel : MonoBehaviour, IDragHandler
{
    Canvas canvas;
    RectTransform rtParent;
    public DirectionType directionType;
    public float minWidth = 100f;
    public float minHeight = 100f;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rtParent = transform.parent.GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta / canvas.scaleFactor;
        switch (directionType)
        {
            case DirectionType.Right:
                ResizeRight(delta.x);
                break;
            case DirectionType.Left:
                ResizeLeft(delta.x);
                break;
            case DirectionType.Bottom:
                ResizeBottom(delta.y);
                break;
            case DirectionType.Top:
                ResizeTop(delta.y);
                break;
        }
    }
    private void ResizeRight(float deltaX)
    {
        float scaleX = rtParent.localScale.x;
        deltaX /= scaleX;
        float newWidth = rtParent.rect.width + deltaX;
        if(minWidth <= newWidth)
        {
            rtParent.offsetMin += new Vector2(deltaX, 0);
        }
    }
    private void ResizeLeft(float deltaX)
    {
        float scaleX = rtParent.localScale.x;
        deltaX /= scaleX;
        float newWidth = rtParent.rect.width - deltaX;
        if (minWidth <= newWidth)
        {
            rtParent.offsetMin += new Vector2(deltaX, 0);
        }

    }
    private void ResizeBottom(float deltaY)
    {
        float scaleY = rtParent.localScale.y;
        deltaY /= scaleY;
        float newHeight = rtParent.rect.height - deltaY;
        if (minHeight <= newHeight)
        {
            rtParent.offsetMin += new Vector2(0, deltaY);
        }
    }
    private void ResizeTop(float deltaY)
    {
        float scaleY = rtParent.localScale.y;
        deltaY /= scaleY;
        float newHeight = rtParent.rect.height + deltaY;
        if (minHeight <= newHeight)
        {
            rtParent.offsetMin += new Vector2(0, deltaY);
        }
    }
}
public enum DirectionType
{
    Right,
    Left,
    Top,
    Bottom
}
