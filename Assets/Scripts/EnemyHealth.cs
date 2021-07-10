using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] ParticleSystem deadBlood;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GameObject.FindGameObjectWithTag("head").GetComponent<SphereCollider>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        Invoke(nameof(Bleed),1);
    }
    private void Bleed()
    {
        ParticleSystem ins = Instantiate(deadBlood, gameObject.transform.position, Quaternion.identity);
        ins.transform.parent = null;
        Destroy(ins.gameObject, ins.main.duration+5);
    }
}
