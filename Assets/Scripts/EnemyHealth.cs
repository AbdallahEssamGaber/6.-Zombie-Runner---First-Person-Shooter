using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int enemyHealth = 3;
    bool isDead = false;


    public bool IsDead ()
    {
        return isDead;
    }
    public void Damage(int damage)
    {
        BroadcastMessage("OnDamageTaken");
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; 
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}