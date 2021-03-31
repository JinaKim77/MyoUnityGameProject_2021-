using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
   
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", rotateSpeed*Time.time);
    }
}
