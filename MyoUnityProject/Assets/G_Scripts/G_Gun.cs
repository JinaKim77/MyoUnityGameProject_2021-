using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
public class G_Gun : MonoBehaviour
{
    public JointOrientation JointObject = null;
    public ThalmicMyo myo; //connect the myo

    //Used to damage enemy
    [SerializeField] float damageEnemy = 10f;
    public float range = 100f;
    public Camera fpsCam;
    //Muzzleflash
    public ParticleSystem muzzleFlash;

    //Eject bullet casing
    public ParticleSystem bulletCasing;

    //Weapon Effect
    public GameObject impactEffect;
    public Transform gunPos;
    public float xGap;
    public float yGap;
    
    private Pose _lastPose = Pose.Unknown;
    private bool sameMovement = false;
    public float gunHeight => gunPos.position.y;
    public float impactForce = 30f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    //Reloading
    public int maxAmmo = 10;  // I can shoot 10 times then pause for 1 second and I can continue shooting.
    public float reloadTime = 1f;
    private int currentAmmo;
    private bool isReloading = false;
    public Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;

        //Do not this play this when the game start
        //When you not actually shooting
        muzzleFlash.Stop();
        bulletCasing.Stop();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

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
    void Update()
    {
        if(isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }


        //When click mouse left
        //Fist
        if(Input.GetButtonDown("Fire1") || (myo.pose == Thalmic.Myo.Pose.Fist)) //Left mouse button
        {
            Shoot();
        }

        //When hold mouse left button continously
        //FingersSpread
        if(Input.GetButton("Fire1") || (myo.pose == Thalmic.Myo.Pose.FingersSpread) && Time.time >= nextTimeToFire) //Left mouse button
        {
            nextTimeToFire = Time.time + 1f / fireRate; // calculates the time until next fire
            Shoot();
        }
    }

    IEnumerator Reload()
    {

        //play the sound
        FindObjectOfType<AudioManager>().Play("Reloading");

        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        bulletCasing.Play();

        currentAmmo--;

        //Vibrate when Shooting
        //myo.Vibrate (VibrationType.Medium);

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if(hit.transform.tag == "Enemy")
            {
                //when shoot something, you should be able to access the script to kill the target!
                Target target = hit.transform.GetComponent<Target>();

                target.TakeDamage(damageEnemy);
            }
            
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //play the sound
            FindObjectOfType<AudioManager>().Play("Shoot");

            //When hit something, display this flare effect
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }//end of shoot()
}
