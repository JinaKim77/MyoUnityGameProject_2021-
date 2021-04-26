using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    //public GameObject pickupEffect;
    Ray ray;
    RaycastHit hit;
    [SerializeField] float pickupDistance = 15f;
    Camera mainCam;
    public Text pickupText;
    G_Gun gunScript;
    public LayerMask layer;


    private void Start()
    {
        mainCam = Camera.main;
        gunScript = GetComponentInChildren<G_Gun>();
        pickupText.enabled = false;
    }
    void Update()
    {
        //rotate the pickup object
       // transform.Rotate(90 * Time.deltaTime, 0,0);
       ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

       if(Physics.Raycast(ray, out hit, pickupDistance, layer))
       {
           pickupText.enabled = true;
           pickupText.text = hit.transform.name.ToString();
           pickupText.enabled = false;

           if(hit.transform.tag == "BulletBox")
           {
               pickupText.enabled = true;
           }
       }
       else{
           pickupText.enabled = false;
       }
    }

    void PickupBulletBox()
    {
        pickupText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //pickupText.enabled = true;
            //pickupText.text = hit.transform.name.ToString();
            //pickupText.enabled = false;

            //Instantiate(pickupEffect, transform.position, transform.rotation);
            //Debug.Log("Collected pickup item");
            FindObjectOfType<GameSession>().addScore(coinValue);
            
            //play the sound
            FindObjectOfType<AudioManager>().Play("Pickup");
            Destroy(gameObject); //This destroy things            
        }
        else{
           pickupText.enabled = false;
       }
    }
}