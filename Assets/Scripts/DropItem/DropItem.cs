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
       
        if (reg.dropItems.Count > 1)
        {
            MultipleDrop();
            return;
        }
        SingleDrop();
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
<<<<<<< HEAD
        if (item == null || trTarget == null) return;
=======
        if (item == null || trTarget == null) return; 
>>>>>>> f002b3e15a773ef55cefff2f3d710a744cf024eb
        item.acceptParentTrans = trTarget;
        item?.transform.SetParent(trTarget);
        item?.LineDisable();
        item?.BlockRaycast(true, 1f);
    }
}
