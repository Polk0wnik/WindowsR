using Drag.SelectItem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContext
{
    public bool HasHitPointerEnter { get; private set; }
    public bool InSelectionFrame { get; private set; }
    public bool IsDraggableItem { get; private set; }
    public bool IsActive { get; private set; }
    
    public void SetHasHitPointerEnter(bool isHit)
    {
        HasHitPointerEnter = isHit;
    }
    public void SetInSelectionFrame(bool isFrame)
    {
        InSelectionFrame = isFrame;
    }
    public void SetIsDraggableItem(bool isDrag)
    {
        IsDraggableItem = isDrag;
    }
    public void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
    public void SetInFrame(Outline line)
    {
        InSelectionFrame = true;
        LineEnable(line);
    }
    public void ResetInFrame(Outline line)
    {
        InSelectionFrame = false;
        LineDisable(line);
    }
    public void LineEnable(Outline line)
    {
        line.enabled = true;
    }
    public void LineDisable(Outline line)
    {
        if (!InSelectionFrame)
            line.enabled = false;
    }
    public void PointerEnter()
    {
        HasHitPointerEnter = true;
    }
    public void PointerExit()
    {
        HasHitPointerEnter = false;
    }
    public void BlockRaycast(bool isBlockRay, float alpha, CanvasGroup canGroup)
    {
        canGroup.alpha = alpha;
        canGroup.blocksRaycasts = isBlockRay;
    }
}
