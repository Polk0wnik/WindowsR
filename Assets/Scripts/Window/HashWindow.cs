using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashWindow : MonoBehaviour
{
    public RectTransform rcTr { get; private set; }
    private void Awake()
    {
        rcTr = GetComponent<RectTransform>();
    }
}
