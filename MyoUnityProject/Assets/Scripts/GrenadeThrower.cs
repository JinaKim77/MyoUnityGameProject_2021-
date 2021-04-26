using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public ThalmicMyo myo; //connect the myo
    public float throwForce = 40f;
    public GameObject grenadePrefab;

    void Update()
    {
        if(Input.GetButtonDown("Fire3") || (myo.pose == Thalmic.Myo.Pose.DoubleTap))//left shift button
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
