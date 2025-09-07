using System;
using UnityEngine;
public class SelectionFrame : MonoBehaviour
{
    public event Func<bool> onPointerEnterUI;
    public event Action<DragItemBase> onAddSelectedItem;
    public event Action onResetSelectedItem;
    public GUISkin skin0;
    private int maxLayer = 50;
    private bool onSelectionStay; 
    private Vector2 startPoint;
    private Vector2 endPoint;
    private RegistrySelectableItems registry;
    private Rect screenSpaceRect;
    private void Awake()
    {
        registry = GetComponent<RegistrySelectableItems>();
    }
    private void OnEnable()
    {
        onResetSelectedItem?.Invoke();
    }
    private void OnGUI()
    {
        GUI.skin = skin0;
        GUI.depth = maxLayer;
        StartSelect();
        StaySelect();
    }
    private void Update()
    {
        SelectEnd();
    }
    private void StartSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
            if (onPointerEnterUI.Invoke()) return;
            onSelectionStay = true;
            onResetSelectedItem?.Invoke(); 
        }
    }    
    private void StaySelect()
    {
        if(Input.GetMouseButton(0) && onSelectionStay) 
        {
            endPoint = Input.mousePosition;
            if (Vector2.Distance(startPoint, endPoint) < 10) return;
            screenSpaceRect = GetScreenRect(startPoint,endPoint);
            DraweFrame(screenSpaceRect);
        }
    }    
    private void SelectEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Input.mousePosition;
            onSelectionStay = false;
            SelectUIElementsInRect(screenSpaceRect);
        }
    }
    private void SelectUIElementsInRect(Rect screen)
    {
        foreach (var item in registry.draggableItems)
        {
            Rect itemRect = GetRectFromItem(item);
            if (screen.Overlaps(itemRect, true) && item.currentItemData.GetType() == typeof(ItemData))
            {
                item?.context.SetInFrame(item.line);
                item?.context.LineEnable(item.line);
                onAddSelectedItem?.Invoke(item);
                screenSpaceRect = Rect.zero;
            }
        }
    }
    private void DraweFrame(Rect screenRect)
    {

        float posX = screenRect.x;
        float posY = Screen.height - screenRect.y - screenRect.height;
        float width = screenRect.width;
        float height = screenRect.height;
        Rect newFrame = new Rect(posX, posY, width, height);
        GUI.Box(newFrame, "");

    }
    private Rect GetScreenRect(Vector2 start, Vector2 end)
    {

        float posX = Mathf.Min(start.x, end.x);
        float posY = Mathf.Min(start.y, end.y);
        float width = Mathf.Abs(end.x - start.x);
        float height = Mathf.Abs(end.y - start.y);
        return new Rect(posX, posY, width, height);
    }
    private Rect GetRectFromItem(DragItemBase item)
    {
        Vector3[] positions = new Vector3[4];
        item.rectTransform?.GetWorldCorners(positions);
        Vector3 start = RectTransformUtility.WorldToScreenPoint(null, positions[0]);
        Vector3 end = RectTransformUtility.WorldToScreenPoint(null, positions[2]);
        float x = Mathf.Min(start.x, end.x);
        float y = Mathf.Min(start.y, end.y);
        float width = Mathf.Abs(end.x - start.x);
        float height = Mathf.Abs(end.y - start.y);
        return new Rect(x, y, width, height);
    }

}
