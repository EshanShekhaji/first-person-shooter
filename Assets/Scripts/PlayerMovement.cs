using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerHeight;
    
    [Header("Movement")]
    public float movementSpeed;
    public float movementMultplier;
    public float airMultplier;
    public float groundDrag;
    public float airDrag;
    float horizontalMovement;
    float verticalMovement;
    Vector3 movementDirection;
    
    bool isWalking;
    bool isSprinting;
    public bool isShooting;
    
    [Header("Sprinting")]
    public float walkSpeed;
    public float sprintSpeed;
    public float acceleration;

    [Header("Jumping")]
    public float jumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    bool isGrounded;
    public LayerMask groundMask;
    
    [Header("Animation")]
    public Animator weaponAnimator;

    [SerializeField] Transform orientation;
    Rigidbody rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    
    private void Update()
    {
        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            isWalking = false;
        } else {isWalking = true;}
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        GetInput();
        ControlDrag();
        ControlSpeed();
        Jump();
        HandleAnimations();
        
        Debug.Log(isShooting);        
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    
    
    
    void GetInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        movementDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
    
    void MovePlayer()
    {
        if(isGrounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * movementMultplier, ForceMode.Acceleration);
            // if (rb.velocity == Vector3.zero)
            // {
            //     weaponAnimator.Play("weapon_idle");
            // } else {weaponAnimator.Play("weapon_walk");}
        } else if (!isGrounded)
        {
            rb.AddForce(movementDirection.normalized * movementSpeed * airMultplier, ForceMode.Acceleration);
        }
    }
    
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        } else { rb.drag = airDrag; }
    }
    
    void ControlSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, acceleration * Time.deltaTime);
        } else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, acceleration);
        }
    }
    
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    void HandleAnimations()
    {
        if (isWalking)
        {
            weaponAnimator.SetBool("isWalking", true);
        } else { weaponAnimator.SetBool("isWalking", false); }
        
        if (isShooting)
        {
            weaponAnimator.SetBool("isShooting", true);
        } else
        {
            weaponAnimator.SetBool("isShooting", false);
        }
    }
}
