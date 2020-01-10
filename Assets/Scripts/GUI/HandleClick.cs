using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HandleClick : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public UnityEvent Click;
    public UnityEvent DoubleClick;
    public UnityEvent MouseExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 1)
            Click.Invoke();
        else if (clickCount >= 2)
            DoubleClick.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit.Invoke();
    }
}
