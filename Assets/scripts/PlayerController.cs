using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] CursorController cursor;

    Rigidbody rb;
    Vector3 curDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb = StandardtRbValues(rb);

        cursor.Init();
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

        curDir = dir;

        transform.Rotate(Vector3.up, cursor.CursorDir.x);
    }
    void FixedUpdate()
    {
        Move(curDir.normalized, speed);
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