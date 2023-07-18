using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Touching))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectZone attackZone;


    Rigidbody2D rb;
    Animator animator;
    public enum WalkableDirection { Left, Right}
    Touching touching;
    private WalkableDirection direction;
    private Vector2 WalkDirectionVector = Vector2.right;

    public WalkableDirection walkableDirection { get { return direction;}
        set { 
            if (direction != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }

            direction = value; }
    }

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set { 
        _hasTarget = value;
            animator.SetBool("Target", value);
        } }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touching = GetComponent<Touching>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touching.IsGround && touching.IsOnWall)
        {
            FlipDirection();
        }
        if (!CanMove) { 
            rb.velocity = new Vector2(walkSpeed * WalkDirectionVector.x,rb.velocity.y);
        }
        else
        {
             rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if(direction == WalkableDirection.Right)
        {
            direction = WalkableDirection.Left;
        }else if (direction == WalkableDirection.Left)
        {
                direction = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Hata");
        }
    }
}
