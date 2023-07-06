using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;

    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
        Vector2 cursorDir = Vector2.zero;
        if (Application.isPlaying)
            cursorDir = CursorController.Instance.CursorDir(rotationSpeed);

        transform.position = target.position + offset;
        transform.rotation = target.rotation;
    }
}