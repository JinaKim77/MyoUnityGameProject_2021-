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
    private Pose _lastPose = Pose.Unknown;
    private G_Gun _gun;
    public CharacterController controller;
    Vector3 velocity;
    public float gravity = 1;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    bool isMoving = false;
    private int xHit = 0;
	private int yHit = 0;
	private int zHit = 0;
	private float MinClamp = -40;
	private float MaxClamp = 40;
    public float jumpHeight = 3f;

  void FixedUpdate ()
	{
		if( Input.GetKeyDown( KeyCode.RightArrow )){
			yHit++;
		}
		if( Input.GetKeyDown( KeyCode.LeftArrow )){
			yHit--;
		}

		
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(xHit*90,Mathf.Clamp(yHit*10, MinClamp, MaxClamp),zHit*90), Time.deltaTime*speed);

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
        
        //Jump with space bar
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

      
    }//end of update method
    
}//end of update()


