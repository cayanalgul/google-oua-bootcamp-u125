using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage != null)
        {
           bool gotHit = damage.Hit(attackDamage);
            if (gotHit) {
                Debug.Log(collision.name + "hit for " + attackDamage);
            }
            
        }
    }
}
