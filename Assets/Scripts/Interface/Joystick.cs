using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform background;
    [SerializeField] RectTransform handle;
    private Vector2 inputVector;

    private void Start()
    {
        background = GetComponentInParent<RectTransform>();
        handle = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        CalculateJoystickInput(eventData);
        //print("In OnDrag");
        //Vector2 position;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position);
        //position.x = (position.x / background.sizeDelta.x);
        //position.y = (position.y / background.sizeDelta.y);

        //inputVector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
        //inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        //handle.anchoredPosition = new Vector2(inputVector.x * (background.sizeDelta.x / 2), inputVector.y * (background.sizeDelta.y / 2));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CalculateJoystickInput(eventData);
        //OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
    private void CalculateJoystickInput(PointerEventData eventData)
    {
        Vector2 position;

        // Calculate local position relative to the background boundary box
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / background.sizeDelta.x);
            position.y = (position.y / background.sizeDelta.y);

            inputVector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Move the handle smoothly
            handle.anchoredPosition = new Vector2(
                inputVector.x * (background.sizeDelta.x / 2),
                inputVector.y * (background.sizeDelta.y / 2)
            );
        }
    }
}