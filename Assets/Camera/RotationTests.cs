using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotationTests : MonoBehaviour
{
    [SerializeField] bool byEuler;
    [SerializeField] Quaternion q;
    [SerializeField] Vector3 euler;

    void Update()
    {
        if (byEuler)
        {
            transform.eulerAngles = euler;
        }
        else
        {
            transform.rotation = q;
        }
    }

}
