using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D),typeof(Touching))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    Touching touchingDirections;
    
    public float CurrentWalkSpeed{
        get {
            if(CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGround)
                    {
                        if (IsRunnig)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
           
        }
    }

    private bool _isMoving = false;
    public bool IsMoving { get
        {
            return _isMoving;
        }

     private
      set{
        _isMoving = value;
        animator.SetBool("isMoving",value);
        } 
     }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunnig{
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }
    
    public bool _isFacingRight = true;

    public bool isFacingRight {get{
        return _isFacingRight;}

    private set{
        if(_isFacingRight != value){
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;

        } 
    }


    Rigidbody2D rb;

    Animator animator;
    
    
    public bool CanMove { get
        {
            return animator.GetBool("canMove");
        } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<Touching>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentWalkSpeed, rb.velocity.y);

        animator.SetFloat("yVelocity",rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) 
    { 
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight){
            isFacingRight = true;
        }
        else if(moveInput.x < 0 && isFacingRight){
            isFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context) 
    {
        if(context.started){
            IsRunnig = true;
        }else if(context.canceled)
        {
            IsRunnig= false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGround && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }
}
