using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotation : MonoBehaviour
{
    [Header("Joysticks")]
    public Joystick movementJoystick;
    public Joystick aimJoystick;

    [Header("joystick Icons")]
    public GameObject shipIcon;
    public GameObject cannonIcon;
    public float iconRotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (aimJoystick.joystickVec.y != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, aimJoystick.joystickVec.normalized);
            cannonIcon.transform.rotation = Quaternion.RotateTowards(cannonIcon.transform.rotation, toRotation, iconRotationSpeed * Time.deltaTime);
        }
        if (movementJoystick.joystickVec.y != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementJoystick.joystickVec.normalized);
            shipIcon.transform.rotation = Quaternion.RotateTowards(shipIcon.transform.rotation, toRotation, iconRotationSpeed * Time.deltaTime);
            if (aimJoystick.joystickVec.y == 0)
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, movementJoystick.joystickVec.normalized);
                cannonIcon.transform.rotation = Quaternion.RotateTowards(cannonIcon.transform.rotation, toRotation2, iconRotationSpeed * Time.deltaTime);
            }
        }
    }
}