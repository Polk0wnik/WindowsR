using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnPanel : DropContentBase
{
    private void Awake()
    {
        trTarget = rc.GetComponent<HashDropContentOnPanel>()?.transform;
    }
}
