using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
    [Header("Cam Params")]
    [SerializeField] Transform target;
    [SerializeField] float dist;
    [SerializeField] Quaternion relativeRot;

    [Space]

    [SerializeField] float maxYPos;
    [SerializeField] float minYPos;
    [SerializeField] float xSens;
    [SerializeField] float ySens;

    [SerializeField] private float currentXRotation = 0f;
    [SerializeField] private float currentYRotation = 0f;

    void LateUpdate()
    {
        if (!target) return;

        Vector2 cursorDir = Vector2.zero;
        if (Application.isPlaying)
            cursorDir = CursorController.Instance.CursorDir(xSens, ySens);

        UpdateCam(cursorDir);
    }

    void UpdateCam(Vector2 moveDir)
    {
        // Get the current target position and camera position and apply distance
        Vector3 targetPos = target.position;
        Vector3 cameraPos = targetPos - transform.forward * dist;

        // Apply rotation around the target based on cursor movement
        currentYRotation += moveDir.x;
        currentYRotation = Mathf.Clamp(currentYRotation, minYPos, maxYPos);
        Quaternion rotation = Quaternion.Euler(moveDir.y, currentYRotation, 0f);

        // Calculate the new camera position
        Vector3 newPos = targetPos + rotation * relativeRot * Vector3.back * dist;

        // Apply the new position to the camera
        transform.position = newPos;
        transform.LookAt(targetPos);
    }

    Quaternion curPointRot => transform.localRotation;
}
