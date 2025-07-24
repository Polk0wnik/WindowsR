using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    RegistrySelectableItems reg;
    RectTransform rc;
    private void Awake()
    {
        reg = FindObjectOfType<RegistrySelectableItems>();
        rc = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        SingleDrop();
        if (reg.dropItems.Count > 1)
        {
            MultipleDrop();
        }
    }
    private void SingleDrop()
    {
        DraggableItem item = reg.currentDrItem;
        Transform trTarget = rc.GetComponentInChildren<ContentWindow>().transform;
        Drop(item, trTarget);
    }


    private void MultipleDrop()
    {
        Transform trTarget = rc.GetComponentInChildren<ContentWindow>().transform;
        foreach(var item in reg.dropItems)
        {
            Drop(item, trTarget);
        }
        reg.dropItems.Clear();
    }
    private static void Drop(DraggableItem item, Transform trTarget)
    {
        if (item == null || trTarget == null) return;
        item.acceptParentTrans = trTarget;
        item?.transform.SetParent(trTarget);
        item?.LineDisable();
        item?.BlockRaycast(true, 1f);
    }
}
