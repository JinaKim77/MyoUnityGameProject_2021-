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
    private int xHit = 0;
	private int yHit = 0;
    public float speed;
	private int zHit = 0;
	private float MinClamp = -50;
	private float MaxClamp = 50;

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
		if( Input.GetKeyDown( KeyCode.RightArrow )){
			yHit++;
		}
		if( Input.GetKeyDown( KeyCode.LeftArrow )){
			yHit--;
		}
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(xHit*90,Mathf.Clamp(yHit*10, MinClamp, MaxClamp),zHit*90), Time.deltaTime*speed);

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
