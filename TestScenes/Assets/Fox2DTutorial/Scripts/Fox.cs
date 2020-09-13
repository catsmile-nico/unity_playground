using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    // Public
    public float speed;

    // Private
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Collider2D standingCollider = null ;
    [SerializeField] Transform groundCheckCollider = null;
    [SerializeField] Transform overheadCheckCollider = null;
    [SerializeField] LayerMask groundLayer = 0;
    
    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.1f;
    float horizontalValue;
    float runSpeedModifier = 2f;
    float crouchSpeedModifier = 0.5f;
    [SerializeField] float jumpPower = 100;
    bool isJumping = false;
    [SerializeField] bool isCrouched = false;
    bool facingRight = true;
    [SerializeField] bool isRunning = false;
    [SerializeField] bool isGrounded = false;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, isJumping, isCrouched);
    }

    void GroundCheck()
    {
        isGrounded = false;
        //check if groundcheckobject colliding with other
        //2d colliders in "Ground" layer
        //if yes (isGround true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;

        //if grounded "Jump" bool in animator disabled
        //if (isGrounded)
        animator.SetBool("Jump", !isGrounded);
    }

    // Update is called once per frame
    void Update()
    {
        //store horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal") ;

        //if LShift pressed, enable isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        //if LShift released, disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            isJumping = true;
        }
        else if (Input.GetButtonUp("Jump"))
            isJumping = false;

        if (Input.GetButtonDown("Crouch"))
            isCrouched = true;
        else if (Input.GetButtonUp("Crouch"))
            isCrouched = false;

        //Set yVelocity in animator
        animator.SetFloat("yVelocity", rb.velocity.y);

    }   

    void Move(float direction, bool jumpFlag, bool crouchFlag)
    {
        #region Jump
        //if player is grounded and space pressed Jump
        if(isGrounded && jumpFlag)
        {
            //isGrounded = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f,jumpPower));
        }
        #endregion

        //Left right value
        float xVal = direction * speed * 100 * Time.fixedDeltaTime;

        #region Crouch
        //check overhead
        if(!crouchFlag)
            if(Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
                crouchFlag = true;
        
        //if crouch, disable standing collider + animate crouching + reduce speed
        //if release, undo the above
        if(isGrounded)
            standingCollider.enabled = !crouchFlag;
        if(crouchFlag)
            xVal *= crouchSpeedModifier;

        animator.SetBool("Crouch", crouchFlag);
            
           
        #endregion

        #region Move & Run
        //set val of x using dir and speed
        
        //if running multiply with modifier
        if (isRunning)
            xVal *= runSpeedModifier;
        //cre vec2 for velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //set player velocity
        rb.velocity = targetVelocity;

        //if looking right and clicked left (flip to left)
        if(facingRight && direction<0)
        {
            transform.localScale = new Vector3(-3,3,3);
            facingRight = false;
        }
        //if looking left and clicked right (flip to right)
        else if(!facingRight && direction>0)
        {
            transform.localScale = new Vector3(3,3,3);
            facingRight = true;
        }

        //idle 0, walking 10, running 20
        //Set float xVelocity according to the x value of the rigidbody velocity
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion

    }
}
