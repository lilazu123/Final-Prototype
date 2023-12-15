using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Collider2D playerCollider;
    private Camera mainCamera;
    private float playerHalfWidth;
    private float playerHalfHeight;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        mainCamera = Camera.main;

        if (playerCollider == null)
        {
            Debug.LogError("Collider2D component not found on the player!");
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }

        // Calculate half-width and half-height of the player's collider
        playerHalfWidth = playerCollider.bounds.extents.x;
        playerHalfHeight = playerCollider.bounds.extents.y;
    }

    void Update()
    {
        if (playerCollider == null || mainCamera == null)
        {
            return; // Exit the update if components are missing
        }

        // Calculate the camera's boundaries in world coordinates
        float minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerHalfWidth;
        float maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerHalfWidth;
        float minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + playerHalfHeight;
        float maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - playerHalfHeight;

        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position based on input
        float newX = Mathf.Clamp(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y + verticalInput * moveSpeed * Time.deltaTime, minY, maxY);

        // Set the new position
        transform.position = new Vector3(newX, newY, transform.position.z);

        // Check vertical movement to trigger animation
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // Vertical movement detected, trigger the corresponding animation
            animator.SetBool("IsJumping", true);
        }
        else
        {
            // No vertical movement, set the IsJumping parameter to false
            animator.SetBool("IsJumping", false);
        }

        // Set horizontal movement animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }
}

