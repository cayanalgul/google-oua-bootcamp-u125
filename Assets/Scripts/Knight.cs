using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Touching))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    Rigidbody2D rb;
   
    public enum WalkDirection { Left, Right}
    Touching touching;
    private WalkDirection direction;
    private Vector2 WalkDirectionVector = Vector2.right;

    public WalkDirection Direction { get { return direction;}
        set { 
            if (direction != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }else if(value == WalkDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }

            direction = value; }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touching = GetComponent<Touching>();
    }

    private void FixedUpdate()
    {
        if (touching.IsGround && touching.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed * WalkDirectionVector.x,rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(direction == WalkDirection.Right)
        {
            direction = WalkDirection.Left;
        }else if (direction == WalkDirection.Left)
        {
                direction = WalkDirection.Right;
        }
        else
        {
            Debug.LogError("Hata");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
