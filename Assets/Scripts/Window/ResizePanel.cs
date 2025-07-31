using UnityEngine;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IDragHandler
{
    public float minWidth = 100;
    public float minHeight = 100;

    public float maxWidth = 1920;
    public float maxHeight = 1080;
    public enum ResizeDirection { Left, Right, Top, Bottom }
    public ResizeDirection direction;

    private RectTransform target;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        target = GetComponentInParent<HashResizyWindow>().rcTr;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta / canvas.scaleFactor;

        switch (direction)
        {
            case ResizeDirection.Left:
                ResizeLeft(delta.x);
                break;
            case ResizeDirection.Right:
                ResizeRight(delta.x);
                break;
            case ResizeDirection.Top:
                ResizeTop(delta.y);
                break;
            case ResizeDirection.Bottom:
                ResizeBottom(delta.y);
                break;
        }
    }

    void ResizeLeft(float dx)
    {
        float scale = target.lossyScale.x;
        dx /= scale;
        float newWidth = target.rect.width - dx;
        if (minWidth <= newWidth)
            target.offsetMin += new Vector2(dx, 0);
    }

    void ResizeRight(float dx)
    {
        float scale = target.lossyScale.x;
        dx /= scale;
        float newWidth = target.rect.width + dx;
        if (minWidth <= newWidth)
            target.offsetMax += new Vector2(dx, 0);
    }

    void ResizeTop(float dy)
    {
        float scale = target.lossyScale.y;
        dy /= scale;
        float newHeight = target.rect.height + dy;
        if (minHeight <= newHeight)
            target.offsetMax += new Vector2(0, dy);
    }

    void ResizeBottom(float dy)
    {
        float scale = target.lossyScale.y;
        dy /= scale;
        float newHeight = target.rect.height - dy;
        if (minHeight <= newHeight)
            target.offsetMin += new Vector2(0, dy);
    }
}
 