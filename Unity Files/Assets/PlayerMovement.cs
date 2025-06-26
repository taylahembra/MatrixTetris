using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Keybinds")]
    public KeyCode upKey = KeyCode.Space;
    public KeyCode downKey = KeyCode.LeftShift;

    [Header("Ground")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    public Transform orientation;
    float horizontalInput, verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public CameraMovement cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        grounded = true;

        PlayerInput();
        SpeedControl();

        // Apply drag
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (cameraController.allowMove)
        {
            if (Input.GetKey(upKey)) MoveUp();
            if (Input.GetKey(downKey)) MoveDown();
        }
    }

    private void MovePlayer()
    {
        // Calc movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void MoveUp()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * moveSpeed * 20f, ForceMode.Force);
    }

    private void MoveDown()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y, rb.velocity.z);
        rb.AddForce(-transform.up * moveSpeed * 10f, ForceMode.Force);
    }
}
