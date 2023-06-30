using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    Rigidbody rb;
    Vector3 curDir;

    bool run;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb = StandardtRbValues(rb);
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            dir += transform.forward;

        if (Input.GetKey(KeyCode.S))
            dir += -transform.forward;

        if (Input.GetKey(KeyCode.A))
            dir += -transform.right;

        if (Input.GetKey(KeyCode.D))
            dir += transform.right;

        run = Input.GetKey(KeyCode.LeftShift);

        curDir = dir;

        transform.Rotate(Vector3.up, CursorController.Instance.CursorDir(rotationSpeed).x);
    }

    void FixedUpdate()
    {
        float s = run ? speed * 2 : speed;
        Move(curDir.normalized, s);
    }

    void Move(Vector3 dir, float speed) => rb.MovePosition(transform.position + dir * speed * Time.deltaTime);

    Rigidbody StandardtRbValues(Rigidbody baseRb)
    {
        baseRb.isKinematic = false;
        baseRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        baseRb.constraints = RigidbodyConstraints.FreezeRotation;
        return baseRb;
    }
}