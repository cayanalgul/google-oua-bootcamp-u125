using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    Animator animator;

    [SerializeField] float _maxHealth = 100f;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public float MinHealth;
    float _health = 100f;
    public float Health
    {
        get
        {
            return _health;
        }
        set {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }


    [SerializeField] public bool _isAlive = true;
    [SerializeField]  private bool isInvincible = false;
    public float invicibilityTimer = 0.25f;
    private float timeSinceHit = 0;

    public bool IsAlive
    {
        get { return _isAlive; }
        set {
            _isAlive = value;
            animator.SetBool("isAlive", true);
            Debug.Log("isAlive set " + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invicibilityTimer)
            {
                isInvincible=false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        Hit(15);
    }


    public bool Hit(float damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
        return true;
    }
}
