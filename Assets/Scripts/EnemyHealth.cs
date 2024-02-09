using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    bool isDead = false;

    public bool IsDead { get { return isDead; } }

    public void TakeDamage(float decreaseHitPoints)
    {
        // tell all enemies on taken damage to attack
        BroadcastMessage("OnDamageTaken");
        // GetComponent<EnemyAI>().OnDamageTaken();

        hitPoints -= decreaseHitPoints;

        if (hitPoints <= 0 && !isDead)
        {
            Die();
            //Destroy(gameObject);
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
