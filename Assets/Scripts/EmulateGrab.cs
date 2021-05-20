using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//********************************************************************************//
//******THIS SCRIPT EMULATES CONTROLLER ROTATION SIMILAR TO A VR CONTROLLER*******//
//********************************************************************************//

//Press the 'C' key (C for Controller) while the Game View is active and rotate the Controller with the left mouse button
//Grab objects by pointing and clicking down the left mouse button
//Scroll up and down the mouse wheel to move the grabbed object in z-axis (forward/backward)

public class EmulateGrab : MonoBehaviour
{
    float controllerSpeedHorizontal = 1.5f;
    float controllerSpeedVertical = 1.5f;
    float controllerYaw = 0.0f;
    float controllerPitch = 0.0f;
    
    public float rotationSpeedMultiplier = 2.0f; //A variable to increase the rotational speed for the controller
    private Transform hitTransform;              //A variable to hold the hit object's transform

    void Update()
    {
        if (Input.GetKey(KeyCode.C)) //When the C key is pressed, we'll rotate the controller with the mouse movements
        {
            controllerYaw += controllerSpeedHorizontal * Input.GetAxis("Mouse X") * rotationSpeedMultiplier;
            controllerPitch += controllerSpeedVertical * Input.GetAxis("Mouse Y") * -rotationSpeedMultiplier;
            transform.localRotation = Quaternion.Euler(controllerPitch, controllerYaw, 0.0f);
        }

    }

}
