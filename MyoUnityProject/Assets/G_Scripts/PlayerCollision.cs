using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject deathEffect;
    private bool isAlive;
    //GameSession session;
    
    private void Start()
    {
         //session = GetComponent<GameSession>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            
            //play the sound
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            //session.isTakingDamage = true;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");
        isAlive = false;

        //call the session manager
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
