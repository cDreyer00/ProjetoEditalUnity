using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform rotationPoint;
    [SerializeField] float sensitivity;

    [Range(-1, 1)]
    [SerializeField] float minRot, maxRot;

    void Update()
    {
        Quaternion prevRot = rotationPoint.localRotation;
        rotationPoint.Rotate(Vector3.left, CursorController.Instance.CursorDir(sensitivity).y);

        if (curPointRot.x < minRot)
            rotationPoint.localRotation = new Quaternion(minRot, prevRot.y, prevRot.z, prevRot.w);

        if (curPointRot.x > maxRot)
            rotationPoint.localRotation = new Quaternion(maxRot, prevRot.y, prevRot.z, prevRot.w);
    }

    Quaternion curPointRot => rotationPoint.localRotation;
}
