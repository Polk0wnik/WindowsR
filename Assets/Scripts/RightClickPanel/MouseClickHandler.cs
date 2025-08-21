using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickHandler : MonoBehaviour, IPointerClickHandler
{
    HashRightClickPanel panel;
    RectTransform rectTR;
    private void Awake()
    {
        panel = FindObjectOfType<HashRightClickPanel>(true);
        rectTR = panel.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetNewPosition(Input.mousePosition);
        }
    }
    public void SetNewPosition(Vector2 point)
    {
        panel?.gameObject?.SetActive(true);
        rectTR.pivot = new Vector2(0, 1);
        rectTR.anchorMin = new Vector2(0, 1);
        rectTR.anchorMax = new Vector2(0, 1);
        rectTR.position = point;
    }    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (panel != null)
            {
                SetNewPosition(eventData.pressPosition);
            }
        }
        else
        {
            if (panel != null)
            {
                panel?.gameObject?.SetActive(false);
            }
        }
    }
}
