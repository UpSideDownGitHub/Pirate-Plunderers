using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    [Header("Shooting Joystick")]
    public bool shooting;
    public bool currentlyShooting;

    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 7; //Makes the joystick area to move bigger. Decrease or increase this
    }

    //Pointer down code 
    public void PointerDown(BaseEventData baseEventData)
    {
        if (shooting)
            currentlyShooting = true;
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        joystick.SetActive(true);
        joystickBG.SetActive(true);
        joystick.transform.position = pointerEventData.position;
        joystickBG.transform.position = pointerEventData.position;
        joystickTouchPos = pointerEventData.position;

    }

    //Drag code 
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }
    //Pointer up code 
    public void PointerUp()
    {
        if (shooting)
            currentlyShooting = false;
        joystick.SetActive(false);
        joystickBG.SetActive(false);
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
        
    }

}
