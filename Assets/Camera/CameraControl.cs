using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
    public Transform target;  // O objeto alvo que a câmera irá focar
    public float rotationSpeed = 5f;

    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
        Vector2 cursorDir = Vector2.zero;
        if (Application.isPlaying)
            cursorDir = CursorController.Instance.CursorDir(rotationSpeed);

        Quaternion rotation = Quaternion.Euler(-cursorDir.y, cursorDir.x, 0);
        offset = rotation * offset;

        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}