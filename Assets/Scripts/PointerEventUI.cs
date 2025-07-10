using System.Collections;
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
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> hitResults = new List<RaycastResult>();
        pointerData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerData, hitResults);
        return hitResults;
    }    
    private bool IsPointerDraggable()
    {
        foreach(RaycastResult resultHit in GetRaycastHitResults())
        {
            if(resultHit.gameObject.GetComponent<DraggableItem>()) return true;
        }
        return false;
    }
}
