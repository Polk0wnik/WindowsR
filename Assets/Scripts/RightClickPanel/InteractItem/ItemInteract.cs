using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInteract : MonoBehaviour, IPointerClickHandler
{
    RectTransform rectTr;
    public TMP_InputField inputField { get; private set; }
    float lastClickTime = 0;
    float doubleClickIntervalTime = 0.3f;
    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
    }
    private void OnEnable()
    {
        //inputField.gameObject.SetActive(false);
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
            Debug.Log("hi");
        }
        lastClickTime = Time.time;
    }
}
