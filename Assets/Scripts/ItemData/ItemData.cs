using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newItem", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string nameItem;
    public Sprite spriteItem;
    public GameObject prefabItem;
    public TextMeshProUGUI textItem;
    public InputField inputFieldItem;
    public ItemType itemType;
}
public enum ItemType
{
    Folder,
    Text,
    Image
}