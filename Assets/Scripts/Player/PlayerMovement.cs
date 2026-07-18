using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

        void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        animator = GetComponentInChildren<Animator>();
        
        controller = GetComponent<CharacterController>();
        if (controller == null) {
            controller = gameObject.AddComponent<CharacterController>();
        }
        
        float sY = transform.lossyScale.y;
        float sX = Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
        
        // Order of assignment is critical to prevent Unity from disabling the controller
        controller.height = 2f / sY;
        controller.radius = 0.3f / sX;
        controller.center = new Vector3(0, 1f / sY, 0);
    }

    void Update()
    {
        // 1. Rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // 2. Ground check
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Small downward force to stick to ground
        }

        // 3. Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        
        // Sprint check
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && v > 0;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        
        // Move character horizontally
        controller.Move(move * currentSpeed * Time.deltaTime);

        // 4. Jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            if (animator != null) {
                animator.SetTrigger("Jump");
            }
        }

        // 5. Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 6. Animation
        if (animator != null) {
            float inputMagnitude = Mathf.Clamp01(new Vector2(h, v).magnitude);
            float animSpeed = 0f;
            
            if (inputMagnitude > 0.1f) {
                animSpeed = isRunning ? 2f : 1f;
            }
            
            // Smoothly interpolate the speed parameter for better transitions
            float currentAnimSpeed = animator.GetFloat("Speed");
            animator.SetFloat("Speed", Mathf.Lerp(currentAnimSpeed, animSpeed, Time.deltaTime * 10f));
        }
    }
}