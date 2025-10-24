using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(ButtonPointDown))]
public class SetBtnEventToPointDown : MonoBehaviour
{
    Button btn;
    ButtonPointDown buttonPointDown;


    private void Awake()
    {
        btn = GetComponent<Button>();
        buttonPointDown = GetComponent<ButtonPointDown>();
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonPointDown.OnPointDown.RemoveAllListeners();
        buttonPointDown.OnPointDown.AddListener(OnPointDown);
        btn.targetGraphic.raycastTarget = false;
    }
    private void OnPointDown(PointerEventData arg0)
    {
        btn.onClick?.Invoke();


    }

    private void OnDestroy()
    {
        buttonPointDown.OnPointDown.RemoveAllListeners();

    }

}
