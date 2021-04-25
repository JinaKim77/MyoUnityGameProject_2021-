using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEffect;
    float countdown;
    bool hasExploded = false;
    public float radius = 5f;
    public float force = 700f;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        
        if( countdown <= 0f && !hasExploded)
        {
            //play the sound
            FindObjectOfType<AudioManager>().Play("GrenadeExplosion");

            Explode();
            //As you don't want it be exploded more than once.
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show explosion effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get nearby object
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        //Find the object you want to destroy
        foreach(Collider nearbyObject in collidersToDestroy)
        {
            //Damage
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if(dest != null)
            {
                dest.Destroy();
            }
        }
        
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                //Add force
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //Remove grenade
        Destroy(gameObject);
    }
}
