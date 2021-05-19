using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class character_Controll : MonoBehaviour
{
    #region var
    
    [Header("int")]
    //jump times
    public int Default_Jump_Time = 1;
    int additionalJumps;
    [Header("bool")]
    //bool
    bool right,
        lift,
        isGrounded;
    [Header("float")]
    //float
    public float jumpForce=1,speed = 5;
    public float checkGroundRadius = .3f;
    public float Gravity = 2.5f;
    public float rememberGroundedFor;
    float velocity = 0;
    float lastTimeGrounded;
    [Header("others")]
    //others
    public LayerMask groundLayer;
    Rigidbody2D rb;
    public Transform GC;
    #endregion

    #region start
    //When Start the game
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        jumpForce *= 10;
        Default_Jump_Time -= 1;
        additionalJumps = Default_Jump_Time;
    }
    #endregion

    #region controll
    //Lift
    public void SetLift(bool lift)
    {
        this.lift = lift;
        if (!lift)
            right = false;
    }
    //Right
    public void SetRight(bool right)
    {
        this.right = right;
        if (!right)
            lift = false;
    }
    //Jump
    public void Jump()
    {
        if ((isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0) && (velocity <= 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }
    #endregion

    #region update
    private void Update()
    {
        CheckIfGrounded();
        BetterJump();
    }
    private void FixedUpdate()
    {
        float x = 0;
        if (right)
            x = 1;
        else if (lift)
            x = -1;
        else
            x = 0;

        float moveBy = x * speed;
        if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("Horizontal") < -0.5f)
            moveBy = speed * Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveBy, velocity);
    }
    #endregion

    #region classes
    public void restartJumpTimes()
    {
        additionalJumps = Default_Jump_Time;
        Debug.Log("restarted jumps");
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(GC.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;
            additionalJumps = Default_Jump_Time;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (Gravity - 1) * Time.deltaTime;
        }
        velocity = rb.velocity.y;

    }
    #endregion

    #region creator
    //by Feny
    //insta : @_f_eny
    //tiktok : @_f_eny
    //my site : https://linktr,.ee/Feny3
    /*
       ______   ______    _    _     _    _
      ||       ||        | \   ||    \\  //
      |----    |------   ||\\  ||     \\//
      ||       ||        || \\ ||      ||
      ||       |______   ||  \\||      ||
    */
    #endregion
}
