using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] EnemyAttack enemyAttack;
    public float hitPoints = 5f;

   public void TakeDamage(float deamage)
    {
        hitPoints -= deamage;
        if (hitPoints <= 0)
        {
            Debug.Log("dead bitch!");
        }
    }

   

 
}
