using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 2f; // Adjust gravity scale for falling
    public Transform groundCheck;
    public Transform barrierCheck; // Declare barrierCheck variable
    public LayerMask groundMask;
    public LayerMask barrierMask; // Declare barrierMask variable

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingBarrier; // Variable to track if player is touching a barrier
    private bool canMoveUpDown = false; // Variable to control whether movement is restricted to left/right or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
        // Check if player is touching a barrier
        isTouchingBarrier = Physics2D.OverlapCircle(barrierCheck.position, 0.5f, barrierMask);

        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1))
        {
            canMoveUpDown = !canMoveUpDown; // Toggle between allowing and restricting up/down movement
        }

        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        float verticalMoveInput = canMoveUpDown ? Input.GetAxisRaw("Vertical") : 0f; // Only allow vertical movement if canMoveUpDown is true
        
        rb.velocity = new Vector2(moveInput * moveSpeed, verticalMoveInput * moveSpeed);

        // Apply gravity if not grounded and not allowed to move up/down
        if (!isGrounded && !canMoveUpDown)
        {
            rb.velocity += Vector2.down * gravityScale * 2; // Apply gravity by adding a downward velocity
        }

        if (isGrounded && canMoveUpDown)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Set vertical velocity to 0
                
            }
        }
        
        CircleCollider2D playerCollider = GetComponent<CircleCollider2D>();

        if (isTouchingBarrier)
        {
            Debug.Log("Player X position: " + transform.position.x);
            Debug.Log("Barrier X position: " + barrierCheck.position.x);
            // If touching a barrier, prevent movement in the direction of the barrier
            if (moveInput > 0 && transform.position.x > barrierCheck.position.x)
            {
                // Player is moving right and touching a barrier on their right, so prevent movement right
                rb.velocity = new Vector2(0f, rb.velocity.y);
                
            }
            else if (moveInput < 0 && transform.position.x < barrierCheck.position.x)
            {
                // Player is moving left and touching a barrier on their left, so prevent movement left
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
                //up
            if (verticalMoveInput > 0 && transform.position.y > barrierCheck.position.y)
            {
                // Player is moving up and touching a barrier above, so prevent movement up
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
            else if (verticalMoveInput < 0 && transform.position.y < barrierCheck.position.y)
            {
                // Player is moving down and touching a barrier below, so prevent movement down
                verticalMoveInput = 0f;
            }
        }


        /* Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        */
    }

}
