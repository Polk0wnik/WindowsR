using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropOnPanel : DropContentBase
{
    private void Awake()
    {
        rc = GetComponent<RectTransform>();
        reg = FindObjectOfType<RegistrySelectableItems>();
        trTarget = rc.GetComponent<HashDropContentOnPanel>()?.transform;
    }
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
