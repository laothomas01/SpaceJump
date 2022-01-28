using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * Things to touch up on when polishing or when the basics have been laid out:
     * -check if player is in air
     * -check if player is grounded
     * -check how long player is in the air and falling.
     * -increase gravity value based on how long it's taking the player to fall
     * -input cancelling?
     * -make sure the player cannot hold the downward dash button
     * -make sure the player cannot hold the side dash button
     * -consider the direction the player is facing for movement purposes.
     */
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    private float horizontalMovement;
    public float downwardDistance;
    public float dashDistance;
    public bool jumpKeyPressed;
    public bool downwardStompPressed;
    public bool sideDashPressed;
    public bool sideDash;
    public bool downDash;
    float timeButtonHeld = 0;
    public float Hold_JumpButton_Maximum_Wait_Time;
    public float Hold_Dash_Button_Maximum_Wait_Time;


    public float maxdashCoolDownTime;
    public float maxdownDashCoolDownTime;
    public float initdashCoolDownTime;
    public float initdownDashCoolDownTime;

    /*
     * JUMP CHECKS
     */
    public static bool jump;
    public static bool inAir;
    //public float raycastHitDistance = 1f;
    /*
     GROUND CHECKS 
     
     */
    private RaycastHit2D hit;
    public bool grounded = false;

    [SerializeField] private LayerMask GroundedMask;


    Vector3 targetVelocity;
    Vector3 lastMoveDir;
    Vector3 downDirection;



    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        jump = false;
        jumpKeyPressed = false;
        downwardStompPressed = false;
        sideDashPressed = false;

    }
    private void Update()
    {

        //GroundCheck();

        if (Input.GetKey(KeyCode.J))
        {

            timeButtonHeld += Time.deltaTime;

            if (timeButtonHeld > Hold_JumpButton_Maximum_Wait_Time)
            {
                jump = false;
            }

            else
            {

                jump = true;

            }

        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            jump = false;
            timeButtonHeld = 0;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {

            timeButtonHeld += Time.deltaTime;

            if (timeButtonHeld > Hold_JumpButton_Maximum_Wait_Time)
            {
                downwardStompPressed = false;
            }

            else
            {
                downwardStompPressed = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            downwardStompPressed = false;
        }

        /* when dash is used, 
         * place a cool down on the ability or find a way to limit the number of dashes. 
         */

        //dash

        //your last movement direction x coordinate is the x coordinate direction you just moved.
        lastMoveDir.x = targetVelocity.x;



        if (Input.GetKey(KeyCode.L) && horizontalMovement > 0 || Input.GetKey(KeyCode.L) && horizontalMovement < 0)
        {
            timeButtonHeld += Time.deltaTime;
            if (timeButtonHeld > Hold_Dash_Button_Maximum_Wait_Time)
            {
                sideDash = false;
            }
            else
            {

                sideDash = true;
            }
        }

        else if (Input.GetKeyUp(KeyCode.L))
        {
            sideDash = false;
            timeButtonHeld = 0;
        }



    }
    private void FixedUpdate()
    {

        if (jump)
        {
            Jump();
        }
        if (downwardStompPressed)
        {
            DownwardDash();
        }
        if (sideDash)
        {
            SideDash();
        }


        Move();


    }
    private void Jump()
    {
        /*
         ADD COOL DOWN TO THIS ABILITY
        */
        if (jump)
        {

            rb.velocity = Vector2.up * jumpForce;

        }

    }
    private void Move()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        targetVelocity = new Vector2(horizontalMovement * moveSpeed * 10f * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = targetVelocity;
    }


    /*
     * GROUND CHECKING
     */
    //private void GroundCheck()
    //{
    //    hit = Physics2D.Raycast(transform.position, -transform.up, raycastHitDistance, GroundedMask);
    //    Debug.DrawRay(transform.position, -transform.up * raycastHitDistance, Color.red);
    //    grounded = hit ? true : false;
    //}

    /*
     * MOVEMENT ABILITIES
     */
    private void DownwardDash()
    {
        /*
        
        ADD A COOL DOWN

        */
        rb.AddForce(Vector2.down * downwardDistance);

    }
    private void SideDash()
    {
        /*
         * ADD A COOL DOWN
         */
        if (horizontalMovement < 0)
        {

            rb.AddForce(Vector2.left * dashDistance);
        }
        else
        {
            rb.AddForce(Vector2.right * dashDistance);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(lastMoveDir, 1);

    }

}
