using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class ButtonPointDown : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent<PointerEventData> OnPointDown;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointDown?.Invoke(eventData);
    }
}
