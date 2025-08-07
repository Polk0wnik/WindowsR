using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickHandler : MonoBehaviour, IPointerClickHandler
{
    HashRightClickPanel panel;
    private void Awake()
    {
        panel = FindObjectOfType<HashRightClickPanel>(true);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (panel != null)
            {
            panel?.gameObject?.SetActive(true);
            panel.transform.position = eventData.pressPosition;
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
