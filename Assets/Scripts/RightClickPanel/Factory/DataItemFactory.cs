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
    private DragItemBase currentItem;
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
        PointerMouseEnter();
    }
    private void PointerMouseEnter()
    {
        if(reg.currentDrItem != null)
        {
            currentItem = reg.currentDrItem;
        }    
    }
    public void CreatItem(ItemData itemData)
    {
        ItemData newItemData = Instantiate(itemData);
        GameObject newItem = Instantiate(newItemData.prefabItem, itemsParentTR.position, itemsParentTR.rotation);
        newItem.transform.SetParent(itemsParentTR);
        DragItemBase newItemDrag = newItem.GetComponent<DragItemBase>();
        newItemDrag.SetDataItem(newItemData);
        reg.AddItem(newItemDrag, newItemData.id);
    }
    public void RemoveItem()
    {
        if (reg != null && currentItem != null)
        {
            reg.RemoveItem(currentItem.currentItemData.id);
            currentItem = null;
            gameObject.SetActive(false);
        }
    }
    public void RenameItem()
    {
        if(currentItem != null && reg != null)
        {

            ItemInteract item = currentItem.GetComponent<ItemInteract>();
            if (item.inputField.gameObject == null) print("item = null");
            gameObject.SetActive(false);
        }
    }
}
