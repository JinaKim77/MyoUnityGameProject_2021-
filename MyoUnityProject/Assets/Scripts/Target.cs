using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float enemyHealth = 50f;
    [SerializeField] int killValue = 10;
    EnemyAI enemyAI;
    public GameObject enemyDeathEffect;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    
    ///Should be able to damage the target(Enemy object)
    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;

        if(enemyHealth <= 0f)
        {
            Instantiate(enemyDeathEffect, transform.position, transform.rotation);

            //play the sound
            FindObjectOfType<AudioManager>().Play("EnemyDeath");

            FindObjectOfType<GameSession>().addScore(killValue);
            
            //Kill the target object!
            EnemyDead();
        }
    }

    void EnemyDead()
    {
        enemyAI.EnemyDeathAnim();

        //Destroy the target object!
        Destroy(gameObject,10);
    }
}
