using System;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string id { get; private set; } = "";
    public string nameItem;
    public Sprite spriteItem;
    public GameObject prefabItem;
    public ItemType itemType;
    public string text;
    public void SetID()
    {
        if (id == "")
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
public enum ItemType
{
    Folder,
    Text,
    Image
}