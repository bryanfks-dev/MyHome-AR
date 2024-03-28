using UnityEngine;
using UnityEngine.EventSystems;

/*
 * TouchFieldController script handle touchscreen process 
 * with swiping screen using finger to do camera and body 
 * rotation.
 */
public class TouchFieldController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 TouchDist;
    
    private Vector2 PointerOld;
    private int PointerId;
    private bool Pressed;

    // Update is called once per frame
    void Update()
    {
        // Check if there is a touch input
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                // Calculate touch distant as rotation velocity
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                // Touchdist with mouse as input
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            // Set Touchdist as vector equivalent to Vector2.zero
            TouchDist = new Vector2();
        }
    }

    // Handler for on pointer down
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }

    // Handler for on pointer up
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
