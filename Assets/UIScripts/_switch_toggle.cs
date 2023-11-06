using UnityEngine;
using UnityEngine.UI;

public class _switch_toggle : MonoBehaviour {
    [SerializeField] RectTransform uiHandleTransform;
    [SerializeField] Color backgroundActiveColor;

    Color backgroundDefaultColor;

    Image backgroundImage;

    Toggle toggle;

    Vector2 handlePosition;

    void Awake(){
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleTransform.anchoredPosition;
        backgroundImage = uiHandleTransform.parent.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;



        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    void OnSwitch(bool on){
        if (on)
        {
            uiHandleTransform.anchoredPosition = handlePosition * -1;

        }
        else
            uiHandleTransform.anchoredPosition = handlePosition;

        backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;

    }

    void OnDestroy(){
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
