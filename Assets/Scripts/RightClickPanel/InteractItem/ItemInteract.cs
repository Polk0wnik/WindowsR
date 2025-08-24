using Assets.Scripts.HASH;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteract : MonoBehaviour, IPointerClickHandler
{
    RectTransform rectTr;
    public GameObject prefabWindow;
    private GameObject window;
    private Transform transParent;
    public TMP_InputField inputField { get; private set; }
    float lastClickTime = 0;
    float doubleClickIntervalTime = 0.3f;
    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
    }
    private void Start()
    {
        transParent = GetComponentInParent<HashMainCanvas>()?.transform;
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
            window = Instantiate(prefabWindow, Vector3.zero, Quaternion.identity);
            window.transform.SetParent(transParent);
            RectTransform transRect = window.GetComponent<RectTransform>();
            transRect.pivot = new Vector2(0, 1);
            transRect.anchorMin = new Vector2(0, 1);
            transRect.anchorMax = new Vector2(0, 1);
            transRect.anchoredPosition = new Vector2(700, -200);
        }
        lastClickTime = Time.time;
    }
}
