using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform camera;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 rot;
    [SerializeField] float maxXRot;
    [SerializeField] float minXRot;

    void LateUpdate()
    {
        if (!target) return;
        if (!camera) return;

        transform.position = target.position;
        camera.transform.localPosition = Vector3.zero + offset;
        camera.LookAt(target);

        Vector2 cursorDir = Vector2.zero;
        if (Application.isPlaying)
            cursorDir = CursorController.Instance.CursorDir(rotationSpeed);

        rot.x += -cursorDir.y * rotationSpeed * Time.deltaTime;
        rot.y += cursorDir.x * rotationSpeed * Time.deltaTime;
        rot.x = Mathf.Clamp(rot.x, minXRot, maxXRot);

        transform.eulerAngles = rot;
    }
}