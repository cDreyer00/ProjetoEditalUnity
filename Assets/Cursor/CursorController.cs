using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CursorController : Singleton<CursorController>
{
    [SerializeField] float sensitivity = 1;

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

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 CursorDir(float sensitivity = 0)
    {
        sensitivity = sensitivity == 0 ? this.sensitivity : sensitivity;
        return CursorDir(sensitivity, sensitivity);
    }

    public Vector2 CursorDir(float xSens, float ySens) => new Vector2(MouseX * xSens, MouseY * ySens);
}
