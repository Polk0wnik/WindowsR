using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataItemFactory : MonoBehaviour
{
    public List<ItemData> items = new();
    Dictionary<ItemType, ItemData> itemsType = new();
    private Transform itemsParentTR;
    RegistrySelectableItems reg;
    private void Awake()
    {
        if(items.Count == 0)
        {
            Debug.Log("items.count = 0");
        }    
        else
        {
            foreach(ItemData item in items)
            {
                if(!itemsType.ContainsKey(item.itemType))
                {
                    itemsType.Add(item.itemType, item);
                }

            }
        }
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    private void OnEnable()
    {
        itemsParentTR = FindObjectOfType<HashDropContentOnPanel>().transform;
    }
    public void CreatItem(ItemData itemData)
    {
        GameObject newItem = Instantiate(itemData.prefabItem, itemsParentTR.position, itemsParentTR.rotation);
        newItem.transform.SetParent(itemsParentTR);
        DragBase drBase = newItem.GetComponent<DragBase>();
        reg.AddItemDrag(drBase);
        reg.AddItemSelectAndDrop(drBase);
    }
}
