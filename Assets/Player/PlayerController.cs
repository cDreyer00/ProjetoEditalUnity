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
    [SerializeField] DirType curDirType;

    Rigidbody rb;
    Vector3 curDir;

    bool run;
    bool canJump;
    bool triggerJump;

    float curAccel;

    bool rotate = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb = StandardRbValues(rb);
    }

    void Update()
    {
        CheckMove();
        if (rotate) Rotate();
        CheckJump();

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            rotate = !rotate;
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void CheckMove()
    {
        Vector3 dir = Vector3.zero;

        bool goForward = Input.GetKey(KeyCode.W);
        bool goBack = Input.GetKey(KeyCode.S);
        bool goLeft = Input.GetKey(KeyCode.A);
        bool goRight = Input.GetKey(KeyCode.D);

        if (goForward)
            dir += transform.forward;

        if (goBack)
            dir += -transform.forward;

        if (goLeft)
            dir += -transform.right;

        if (goRight)
            dir += transform.right;

        run = Input.GetKey(KeyCode.LeftShift);

        curDir = dir;
        curDirType = CheckDirType(goForward, goBack, goLeft, goRight);
    }

    void Move()
    {
        bool isMoving = curDir.magnitude != 0;
        animController.SetBool("move", isMoving);

        Vector2 dir = DirTypeToVector(curDirType);
        animController.SetFloat("x", dir.x);
        animController.SetFloat("z", dir.y);

        if (isMoving)

            if (!isMoving)
                SlowDown();
            else
                SpeedUp();

        float s = run ? runSpeed : speed;
        s *= curAccel;
        rb.MovePosition(transform.position + curDir.normalized * s * Time.deltaTime);
    }

    DirType CheckDirType(bool goF, bool goB, bool goL, bool goR)
    {
        if (!goF && !goB && !goL && !goR)
            return DirType.None;

        if (goF && goL)
            return DirType.FL;

        if (goF && goR)
            return DirType.FR;

        if (goB && goL)
            return DirType.BL;

        if (goB && goR)
            return DirType.BR;

        if (goF)
            return DirType.F;

        if (goB)
            return DirType.B;

        if (goL)
            return DirType.L;

        if (goR)
            return DirType.R;

        return DirType.None;
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

    Vector2 DirTypeToVector(DirType dt)
    {
        Vector2 v = dt switch
        {
            DirType.F => new Vector2(0, 1),
            DirType.FL => new Vector2(-1, 1),
            DirType.FR => new Vector2(1, 1),
            DirType.B => new Vector2(0, -1),
            DirType.BL => new Vector2(-1, -1),
            DirType.BR => new Vector2(1, -1),
            DirType.L => new Vector2(-1, 0),
            DirType.R => new Vector2(1, 0),
            _=> Vector2.zero
        };

        return v;
    }
}

public enum DirType
{
    None,
    F, FL, FR,
    B, BL, BR,
    L, R,
}
