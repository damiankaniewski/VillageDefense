using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private Animator animator;

    public float moveSpeed = 6;
    private float speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = Vector3.zero;
        if (isDead())
        {
            direction = Vector3.zero;
        }
        else
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isMoving", false);
                speed = 2 * moveSpeed;
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isMoving", true);
                speed = moveSpeed;
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", false);
        }
    }

    bool isDead()
    {
        if (animator.GetBool("isDead") == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}