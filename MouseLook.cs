using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
  
    public float mouseSensitivity = 100f;
    public float mouseSensX = 2;
    public float mouseSensY = 2;

    public Transform playerBody;
    float xRotation = 0f;
    public Transform cursorthing;

    public bool clampingOn = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        mouseSensX = 2;
        mouseSensY = 2;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       

        float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity * mouseSensX) * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity * mouseSensY) * Time.deltaTime;

        if (clampingOn == true)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
            
        }
        else
        {

            xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f); this is to stop looking past these 2 angles
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
            
        }
        
    }

    public void AdjustSensX(float newSensX)
    {

        mouseSensX = newSensX;
    }

    public void AdjustSensY(float newSensY)
    {

        mouseSensY = newSensY;
    }
}
