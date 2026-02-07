using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Setting")]
    public float moveSpeed = 10f;
    public float sprintSpeed = 20f;
    public float turnSpeed = 180f;
    public float xRange = 10;
    public GameObject foodPrefeb;
    public Transform shootPos;

    [Header("Current State")]
    private float verticalInput = 0.0f;
    private float horizontalInput = 0.0f;
    private InputAction moveAction;
    private InputAction shootAction;
    private float shootCoolDown = .5f;
    bool isMove = true;
    bool isSprint;
    bool isGrounded;
    bool isShoot;
    Rigidbody rb;
    private float nextTime = 0;
    private float nextShootTime = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
        shootAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }
    void Move()
    {
        var horizontalInput = moveAction.ReadValue<Vector2>().x;
            transform.Translate(horizontalInput * moveSpeed * Time.deltaTime * Vector3.right);

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        
        // float v = 0f;
        // if (Keyboard.current != null)
        // {
        //     v = (Keyboard.current.aKey.isPressed ? 1f:0f) - (Keyboard.current.dKey.isPressed ? 1f:0f);
        // }
        // Vector3 moveRight = transform.right * v * moveSpeed;
        // rb.linearVelocity = new Vector3(moveRight.x, rb.linearVelocity.y, moveRight.z);
    }

    void Shoot()
    {
        isShoot = shootAction.IsPressed();
        if(isShoot && Time.time > nextShootTime)
        {
            Instantiate(foodPrefeb, shootPos.position, Quaternion.identity);
            nextShootTime = Time.time + shootCoolDown;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, 1f);
        // Gizmos.color = Color.orange;
        // Gizmos.DrawLine(transform.position, Camera.main.transform.position);
        Vector3 left = new Vector3(xRange, transform.position.y, transform.position.z);
        Vector3 right = new Vector3(-xRange, transform.position.y, transform.position.z);
        Gizmos.DrawLine(left,right);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(left,1f);
        Gizmos.DrawWireSphere(right,1f);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
