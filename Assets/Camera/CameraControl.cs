using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControl : Singleton<CameraControl>
{
    [SerializeField] Transform target;
    [SerializeField] Transform camera;
    [SerializeField] float rotationSpeed = 5f;

    [SerializeField] Vector3 posOffset;
    [SerializeField] Vector3 camPosOffset;
    [SerializeField] Vector3 rot;
    [SerializeField] float maxXRot;
    [SerializeField] float minXRot;

    void LateUpdate()
    {
        if (!target) return;
        if (!camera) return;

        transform.position = target.position + posOffset;
        camera.transform.localPosition = Vector3.zero + camPosOffset;
        camera.LookAt(transform.position);

        Vector2 cursorDir = Vector2.zero;
        if (Application.isPlaying)
            cursorDir = CursorController.Instance.CursorDir(rotationSpeed);

        rot.x += -cursorDir.y * rotationSpeed * Time.deltaTime;
        rot.y += cursorDir.x * rotationSpeed * Time.deltaTime;
        rot.x = Mathf.Clamp(rot.x, minXRot, maxXRot);

        transform.eulerAngles = rot;
    }

    public Vector3 GetPointingDir() => transform.forward;
}