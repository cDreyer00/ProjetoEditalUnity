using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField] float acceleration;
    [SerializeField] float maxAcceleration;
    [SerializeField] float speed;
    [SerializeField] float runSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Animator animController;

    Rigidbody rb;
    Vector3 curDir;

    bool run;
    bool canJump;
    bool triggerJump;

    float curAccel;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb = StandardRbValues(rb);
    }

    void Update()
    {
        CheckMove();
        Rotate();
        CheckJump();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void CheckMove()
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
    }

    void Move()
    {
        bool isMoving = curDir.magnitude != 0;
        
        animController.SetBool("move", isMoving);
        if (isMoving)
            animController.SetBool("run", run);

        if (!isMoving)
            SlowDown();
        else
            SpeedUp();

        float s = run ? runSpeed : speed;
        s *= curAccel;
        rb.MovePosition(transform.position + curDir.normalized * s * Time.deltaTime);
    }

    void SpeedUp()
    {
        curAccel += acceleration * Time.deltaTime;
        curAccel = Mathf.Min(curAccel, maxAcceleration);
    }

    void SlowDown()
    {
        curAccel -= acceleration * Time.deltaTime;
        curAccel = Mathf.Max(curAccel, 0);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up, CursorController.Instance.CursorDir(rotationSpeed).x);
    }

    void CheckJump()
    {
        canJump = Physics.Raycast(transform.position * 0.5f, Vector3.down, 0.55f);

        if (!canJump) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            triggerJump = true;
            canJump = false;
        }
    }

    void Jump()
    {
        if (!triggerJump) return;
        triggerJump = false;

        Debug.Log("Jump");

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    Rigidbody StandardRbValues(Rigidbody baseRb)
    {
        baseRb.isKinematic = false;
        baseRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        baseRb.constraints = RigidbodyConstraints.FreezeRotation;
        return baseRb;
    }

}