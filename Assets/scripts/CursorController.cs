using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursorController
{
    [SerializeField] float sensitivity;

    float _mouseX = 0;
    float MouseX
    {
        get
        {
            _mouseX = Input.GetAxis("Mouse X");
            return _mouseX;
        }
    }

    float _mouseY = 0;
    float MouseY
    {
        get
        {
            _mouseY = Input.GetAxis("Mouse Y");
            return _mouseY;
        }
    }

    public void Init()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 CursorDir => new Vector3(MouseX, MouseY, 0) * sensitivity;

}
