using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{


    //sensibilidad del mouse
    public float mouseSensitivity = 80f;

    public Transform playerBody;

    float xRotation = 0;

    void Start()
    {
        //no mostrar el cursor del mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //rotacion eje x
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        //rotacion eje y
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //si no ponia -= cada que movia el raton hacia arriba, la camara iba hacia abajo y viceversa xd
        xRotation -= mouseY;

        //ajustar limites de rotacion
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transformacion de la rotacion
        transform.localRotation = Quaternion.Euler(xRotation,0,0);

        playerBody.Rotate(Vector3.up * mouseX);
       
    }
}
