using Assets.Scripts.HASH;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteract : MonoBehaviour, IPointerClickHandler
{
    public WindowData curData;
    DragItemBase curWindow;
    DragItemBase curItem;
    DragItemBase curMinWindow;
    RegistrySelectableItems reg;
    public GameObject prefabWindow;
    private GameObject window;
    private GameObject newMinWindow;
    private Transform transParentCanvas;
    private Transform transParentGridWindow;
    public TMP_InputField inputField { get; private set; }
    float lastClickTime = 0;
    float doubleClickIntervalTime = 0.3f;
    string curID = "";
    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        curItem = GetComponent<DragItemBase>();
        reg = FindObjectOfType<RegistrySelectableItems>();
    }
    private void Start()
    {
        transParentCanvas = GetComponentInParent<HashMainCanvas>(true)?.transform;
        transParentGridWindow = FindObjectOfType<HashGridWindow>(true)?.transform;
    }
    private void OnEnable()
    {
        inputField.gameObject.SetActive(false);
        //rectTr = GetComponent<RectTransform>();
        //rectTr.localScale = new Vector2(1, 1);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            HandleLeftMouseButtonClick();
        }
    }
    private void HandleLeftMouseButtonClick()
    {
        if(Time.time - lastClickTime <= doubleClickIntervalTime)
        {
            CreateWindow();
            CreateMinWindow();
        }
        lastClickTime = Time.time;
    }
    private void CreateMinWindow()
    {
        if (window != null)
        {
            if(!reg.itemsID.ContainsKey(curID))
            {
                WindowData newData = Instantiate(curData);
            curID = curItem.currentItemData.id;
            newMinWindow = Instantiate(window);
            DragItemBase minWindow = newMinWindow.GetComponent<DragItemBase>();
                minWindow.SetDataItem(newData);
                minWindow.currentItemData.SetID(curID);
            reg.AddMiniWindow(minWindow,curID);
            newMinWindow.transform.SetParent(transParentGridWindow);
            window.SetActive(true);
            newMinWindow.SetActive(true);
            }
            else
            {
                window.SetActive(true);
                newMinWindow.SetActive(true);
            }
        }
    }
    private void CreateWindow()
    {
        if (window == null && transParentCanvas != null && prefabWindow != null)
        {
            SetNewIdWindow();
            SetTransformParentWindow();
        }
    }

    private void SetTransformParentWindow()
    {
        window.transform.SetParent(transParentCanvas);
        RectTransform transRect = window.GetComponent<RectTransform>();
        transRect.pivot = new Vector2(0, 1);
        transRect.anchorMin = new Vector2(0, 1);
        transRect.anchorMax = new Vector2(0, 1);
        transRect.anchoredPosition = new Vector2(700, -200);
    }

    private void SetNewIdWindow()
    {
        WindowData newData = Instantiate(curData);
        window = Instantiate(prefabWindow);
        curWindow = window.GetComponent<DragItemBase>();
        curWindow.SetDataItem(newData);
        string newId = curItem.currentItemData.id;
        curWindow.currentItemData.SetID(newId);
        reg.AddWindow(curWindow, newId);

    }


}
