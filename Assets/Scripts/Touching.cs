using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touching : MonoBehaviour
{
    Animator animator;

    public ContactFilter2D contactFilter;
    CapsuleCollider2D touchingCol;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float cielingDistance = 0.05f;


    RaycastHit2D[] groundHit = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded;
    public bool IsGround
    {
        get
        {
            return _isGrounded;
        }
        private set {
            _isGrounded = value;
            animator.SetBool("isGrounded", value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool("isOnWall", value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool("isOnCeiling", value);
        }
    }


    

    private void Awake() {

       touchingCol = GetComponent<CapsuleCollider2D>();
       animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        IsGround = touchingCol.Cast(Vector2.down, contactFilter, groundHit, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, contactFilter,wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, cielingDistance) > 0;
    }

}
