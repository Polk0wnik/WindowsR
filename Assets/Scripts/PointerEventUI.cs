using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEventUI : MonoBehaviour
{
    private SelectionFrame frame;
    private void Awake()
    {
        frame = GetComponent<SelectionFrame>();
    }
    private void OnEnable()
    {
        frame.onPointerEnterUI += IsPointerDraggable;
    }
    private void OnDisable()
    {
        frame.onPointerEnterUI -= IsPointerDraggable;
    }
    private List<RaycastResult> GetRaycastHitResults()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        }; 
        List<RaycastResult> hitResults = new();
        EventSystem.current.RaycastAll(pointerData, hitResults);
        return hitResults;
    }    
    private bool IsPointerDraggable()
    {
        foreach (var hit in GetRaycastHitResults())
            if (hit.gameObject.GetComponent<DraggableItem>()) return true;
        return false;
    }
}
