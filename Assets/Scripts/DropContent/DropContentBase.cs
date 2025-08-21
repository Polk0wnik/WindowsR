using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropContentBase : MonoBehaviour, IDropHandler
{
    protected RegistrySelectableItems reg;
    protected RectTransform rc;
    protected Transform trTarget;
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (reg.dropItems.Count > 1)
        {
            MultipleDrop(trTarget);
        }
        else if(reg.currentDrItem != null)
        {
            DragItemBase item = reg.currentDrItem;
            SingleDrop(trTarget, item);
        }
    }
    private void SingleDrop(Transform trTarget, DragItemBase item)
    {
        if(item.gameObject.layer == 6)
        Drop(item, trTarget);
    }


    private void MultipleDrop(Transform trTarget)
    {
        foreach(var item in reg.dropItems)
        {
            Drop(item, trTarget);
        }
        reg.dropItems.Clear();
    }
    private static void Drop(DragItemBase item, Transform trTarget)
    {
        if (item == null || trTarget == null) return;
        item.acceptParentTrans = trTarget;
        item?.transform.SetParent(trTarget);
        item?.context.LineDisable(item.line);
        item?.context.BlockRaycast(true, 1f, item.canGroup);
    }
}
