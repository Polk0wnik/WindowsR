using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnWindow : DropContentBase
{
    private void Awake()
    {
        rc = GetComponent<RectTransform>();
        trTarget = rc.GetComponentInChildren<HashDropContentOnWindow>()?.transform;
        reg = FindObjectOfType<RegistrySelectableItems>();

    }
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
