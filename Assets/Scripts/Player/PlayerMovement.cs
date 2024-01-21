using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float gravityStrenght;

    [Header("Ground movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxMoveSpeed;
    [SerializeField]
    private float groundedDrag;
    [SerializeField]
    private float dashForce;
    [SerializeField]
    private float dashCooldown;
    [SerializeField]
    public bool hasDash;
    [SerializeField]
    private float groundCheckLenght;

    [Header("Air movement")]
    [SerializeField]
    private float airMultiplier;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int airJumps;
    

    [Header("Slope movement")]
    [SerializeField]
    private float maxSlopeAngle;

    [Header("Air drop")]
    [SerializeField]
    private float dropForce;
    [SerializeField]
    private float groundPoundDistance;

    [Header("movement variables")]
    [SerializeField]
    private Transform orientation; // orientation tesiog tuscias objektas kuris nesisuka i virsu ar apacia tik i sonus

    #region -Movement private variables-

    private Vector3 moveDir;
    private float maxMoveSpeedConst;
    private float maxMoveSpeedInterp;
    private float interpSpeed = 0f;
    private bool isGrounded;

    private bool isDashing;
    private float dashCooldownTimer;
    private bool startDashCooldown;

    private int airJumpCounter;
    private bool isDropping = false;
    private RaycastHit airDropDistance;

    private Rigidbody rb;
    private RaycastHit slopeHit;
    private Vector3 gravityDir;

    #endregion

    #region -Movement inputs-

    private float verticalInput;
    private float horizontalInput;
    private bool jumpInput;
    private bool dashInput;
    private bool airDropInput;

    #endregion

    private void PlayerInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
        airDropInput = Input.GetKeyDown(KeyCode.LeftControl);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        airJumpCounter = airJumps;

        gravityDir = new Vector3(0f, -gravityStrenght, 0f);

        maxMoveSpeedConst = maxMoveSpeed;
        if (LevelVarTransfer.Instance != null)
        {
            hasDash = LevelVarTransfer.Instance.hasDash;
        }
        
    }

    private void Update()
    {
        PlayerInput();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckLenght);

        PlayerGrounded();
        PlayerLimitSpeed();
        Jump();
        Dash();
        DashCooldownHandler();
        AirDrop();
    }

    private void FixedUpdate()
    {
        PlayerShmovement();
        rb.AddForce(gravityDir, ForceMode.Force);
        
    }

    #region Player movement

    private void PlayerShmovement()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;


        //slope movement
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed, ForceMode.Force);

            gravityDir = -slopeHit.normal * gravityStrenght;
            
        }
        if (!OnSlope())
        {
            gravityDir = new Vector3(0f, -gravityStrenght, 0f);
        }

        //ground movement
        if (isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Force);
        }

        //air movement
        if (!isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }
    }

    private void PlayerGrounded()
    {
        if (isGrounded)
        {
            rb.drag = groundedDrag;
            airJumpCounter = airJumps;
        }
        else 
        {
            rb.drag = 0;
        }
    }

    private void PlayerLimitSpeed()
    {
        //player speed without the y
        Vector3 playerSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limiting the speed on slopes
        if (OnSlope())
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        //canceling the limit on speed and lerping it back
        if (isDashing)
        {
            
            interpSpeed += Time.deltaTime;
            maxMoveSpeedInterp = Mathf.Lerp(1000f, maxMoveSpeedConst, interpSpeed * 5f);
            maxMoveSpeed = maxMoveSpeedInterp;

            if (maxMoveSpeedConst == maxMoveSpeedInterp)
            {
                isDashing = false;
                interpSpeed = 0f;
            }
        }

        //limiting the speed on the ground
        if (playerSpeed.magnitude > maxMoveSpeed)
        {
            Vector3 limitedPlayerSpeed = playerSpeed.normalized * maxMoveSpeed;
            rb.velocity = new Vector3(limitedPlayerSpeed.x, rb.velocity.y, limitedPlayerSpeed.z);
        }
    }

    private void Jump()
    {
        if (isGrounded && jumpInput)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (!isGrounded && jumpInput && airJumpCounter > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            airJumpCounter--;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Dash()
    {
        if (dashInput && rb.velocity.magnitude > 1f && dashCooldownTimer <= 0f && hasDash)
        {
            isDashing = true;
            startDashCooldown = true;
            dashCooldownTimer = dashCooldown;
            rb.AddForce(moveDir.normalized * dashForce, ForceMode.Impulse);
        }
        
    }

    private void DashCooldownHandler()
    {
        if (startDashCooldown)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (dashCooldownTimer >= dashCooldown)
        {
            startDashCooldown = false;
        }
        
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, groundCheckLenght + 0.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);

            if (angle < maxSlopeAngle && angle != 0)
            {
                
                return true;
            }

        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void AirDrop()
    {

        if(airDropInput && !isGrounded)
        {
            Physics.Raycast(transform.position, Vector3.down, out airDropDistance);
            rb.AddForce(Vector3.down * dropForce, ForceMode.Impulse);
            isDropping = true;
            Debug.Log(airDropDistance.distance);
        }
        if (isGrounded && isDropping && airDropDistance.distance > 3.4f)
        {
            GroundPound();
            isDropping = false;
        }
    }

    private void GroundPound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        foreach (Collider collider in colliders)
        {
            
            BaseEnemy hitEnemies = collider.GetComponent<BaseEnemy>();
            if (hitEnemies != null)
            {
                
                hitEnemies.TakeDamage(25f); // damage enemies
                hitEnemies.GetComponent<NavMeshAgent>().enabled = false;

                if (!hitEnemies.GetComponent<Rigidbody>())
                {
                    Rigidbody enemiesRb = hitEnemies.AddComponent<Rigidbody>();

                    // setting defaults for created rigid body
                    enemiesRb.drag = 5f;
                    enemiesRb.interpolation = RigidbodyInterpolation.Interpolate;
                }
                

            }
            // launch rigid bodies up
            Rigidbody otherRigidBody = collider.GetComponent<Rigidbody>();
            if (otherRigidBody != null && otherRigidBody != rb)
            {
                otherRigidBody.AddForce(Vector3.up * 50f, ForceMode.Impulse);
            }
        }
    }

    public void addAirJump()
    {
        airJumpCounter++;
    }

    #endregion


}
