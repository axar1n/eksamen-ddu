using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canMoveUpDown = false; // Variable to control whether movement is restricted to left/right or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            canMoveUpDown = !canMoveUpDown; // Toggle between allowing and restricting up/down movement
        }

        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        float verticalMoveInput = canMoveUpDown ? Input.GetAxisRaw("Vertical") : 0f; // Only allow vertical movement if canMoveUpDown is true
        rb.velocity = new Vector2(moveInput * moveSpeed, verticalMoveInput * moveSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
