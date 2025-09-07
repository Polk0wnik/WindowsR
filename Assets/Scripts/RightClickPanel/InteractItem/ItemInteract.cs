using Assets.Scripts.HASH;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteract : MonoBehaviour, IPointerClickHandler
{
    DragItemBase curWindow;
    RectTransform rectTr;
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
        curWindow = GetComponent<DragItemBase>();
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
            CreateAndOpenItemInWindow();
            CreateMenuInGrid();
        }
        lastClickTime = Time.time;
    }
    private void CreateMenuInGrid()
    {
        if (window != null)
        {
            if(!reg.itemsID.ContainsKey(curID))
            {
            curID = curWindow.currentItemData.id;
            newMinWindow = Instantiate(window);
            DragItemBase minWindow = newMinWindow.GetComponent<DragItemBase>();
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
    private void CreateAndOpenItemInWindow()
    {
        if (window == null && transParentCanvas != null && prefabWindow != null)
        {
            SetNewIdWindow();
            SetTransformParentWindow();
        }

        if (window != null)
            window.SetActive(true);
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
        window = Instantiate(prefabWindow, Vector3.zero, Quaternion.identity);
        DragItemBase newWindow = window.GetComponent<DragItemBase>();

        string newId = curWindow.currentItemData.id;
        reg.AddWindow(newWindow, newId);
        newWindow.currentItemData.SetID(newId);
    }


}
