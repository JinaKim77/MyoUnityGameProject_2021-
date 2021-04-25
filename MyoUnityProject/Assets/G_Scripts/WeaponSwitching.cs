using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Transform gunPosition;
    public float gunHeight => gunPosition.position.y;
    public ThalmicMyo myo; //connect the myo

    void Start()
    {
        selectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if( (Input.GetAxis("Mouse ScrollWheel") > 0f) || (myo.pose == Thalmic.Myo.Pose.WaveOut) )//scroll up
        {
            if(selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if( (Input.GetAxis("Mouse ScrollWheel") < 0f) || (myo.pose == Thalmic.Myo.Pose.WaveIn))//scroll down
        {
            if(selectedWeapon <= 0 )
                selectedWeapon = transform.childCount-1;
            else
                selectedWeapon--;
        }

        //press 1 key on a keyboard
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        //press 2 key on a keyboard
        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 )
        {
            selectedWeapon = 1;
        }

        //press 3 key on a keyboard
        if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        //press 4 key on a keyboard
        if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        //press 5 key on a keyboard
        if(Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }

        if(previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }


    }

    void selectWeapon()
    {
        //play the sound
        FindObjectOfType<AudioManager>().Play("weaponSwitching");

        int i = 0;
        foreach(Transform weapon in transform)
        {
            if( i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }
}
