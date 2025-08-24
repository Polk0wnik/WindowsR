using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCustom : MonoBehaviour
{
    public Texture2D defaultTextCursor;
    public Texture2D clickCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    private void Start()
    {
        SetCustomCursor(defaultTextCursor);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetCustomCursor(clickCursor);
        }
        if(Input.GetMouseButtonUp(0))
        {
            SetCustomCursor(defaultTextCursor);
        }
    }
    public void SetCustomCursor(Texture2D texture2D)
    {
        Cursor.SetCursor(texture2D, hotSpot, cursorMode);
    }
}
