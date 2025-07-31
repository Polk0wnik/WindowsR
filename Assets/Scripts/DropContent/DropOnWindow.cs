using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnWindow : DropContentBase
{
    private void Awake()
    {
        trTarget = rc.GetComponent<HashDropContentOnWindow>()?.transform;
    }
}
