using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class dragUIHandler : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private Canvas current_canvas;

    private RectTransform DragWindow;

    private Rect dragWindow_size, canvas_size;

    private void Start()
    {
        DragWindow = GetComponent<RectTransform>();
        dragWindow_size = GetComponent<RectTransform>().rect;
        canvas_size = current_canvas.GetComponent<RectTransform>().rect;
    }

    private void Update()
    {

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        DragWindow.anchoredPosition += eventData.delta;

        DragWindow.anchoredPosition = new Vector2(Mathf.Clamp(DragWindow.anchoredPosition.x, - ((dragWindow_size.width - canvas_size.width)/2), (dragWindow_size.width - canvas_size.width) / 2),
                                                  Mathf.Clamp(DragWindow.anchoredPosition.y, -((dragWindow_size.height - canvas_size.height) / 2), (dragWindow_size.height - canvas_size.height) / 2)
                                                 );
    }

}
