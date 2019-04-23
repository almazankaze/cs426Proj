using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* SOME TUTORIALS USED!!!!!!
 * SEE README FOR DETAILS
 */

public class CameraControl : NetworkBehaviour
{
    // the mouse sensitivity
    private float mouseSensitivity = 150;

    // reference to player's transform component
    public Transform playerTrans;

    // will be used to clamp the camera
    private float xAxisClamp;

    // when game starts
    private void Awake()
    {
        xAxisClamp = 0.0f;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {   
        CameraRotation();
    }

    private void CameraRotation()
    {
        // get the input of the mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // keeps record of amount of rotation done on the camera
        xAxisClamp += mouseY;

        // prevents camera from rotating above 90/look up
        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotation(270.0f);
        }
        // prevents camera from rotating below -90/look down
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotation(90.0f);
        }

        // actually rotate the camera vertically
        transform.Rotate(Vector3.left * mouseY);

        // acyually rotate the camera horizontally and rotate the player
        playerTrans.Rotate(Vector3.up * mouseX);
    }

    // directly clamp xAxis to specific value to prevent overshooting rotation value
    private void ClampXAxisRotation(float val)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = val;
        transform.eulerAngles = eulerRotation;
    }

}
