using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
using System;

public class PlayerMovement : MonoBehaviour
{
    public JointOrientation JointObject = null;
    private bool sameMovement = false;
    public GameObject myo = null;
    public float speed = 12f;
    public bool isUsingMyo; //is using myo?
    //private Thalmic.Myo.Pose _lastPose;
    private Pose _lastPose = Pose.Unknown;
    private G_Gun _gun;

    public CharacterController controller;
    Vector3 velocity;
    public float gravity = 1;
    public float xGap;
    public float yGap;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    bool isMoving = false;
    public float jumpHeight = 3f;

   void FixedUpdate ()
	{
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.WaveOut) {
                sameMovement = true;
            }
          }
        else {
          sameMovement = false;
        }



        var JointObject =  GameObject.Find("Stick");
        float x = JointObject.transform.rotation.eulerAngles.x;
        float y = JointObject.transform.rotation.eulerAngles.y;
        float z = JointObject.transform.rotation.eulerAngles.z;
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        if (0 + xGap < x && x < 180)
        {
            moveVertical = 1;
        }
        else if (180  < x && x < 360 - xGap)
        {
            moveVertical = -1;
        }


        if (0 + yGap < y && y < 180)
        {
            moveHorizontal = 1;
        }
        else if (180  < y && y < 360 - yGap)
        {
            moveHorizontal = -1;
        }
    }

    private void Update()
    {
        //Player movement will be controlled by keyboard and mouse
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward *z;

        controller.Move(move * speed * Time.deltaTime);

        //Check character controller is moving or not
        if(controller.velocity.magnitude > 0)
        {
            Debug.Log("Player moving");
            isMoving=true;
            
        }else{
            isMoving=false;
        }
        
        //if the player is moving, then play this sound!
        if(isMoving)
        {
            //play the sound
            //FindObjectOfType<AudioManager>().Play("Walking");
        }

        //Jump with space bar
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
 
     //With Myo
     if (isUsingMyo)  {

         //Do something

     }

      // Enable/Disable Myo
      if (Input.GetKeyDown(KeyCode.P)) isUsingMyo = !isUsingMyo;
      
    }//end of update method
    
}//end of update()


