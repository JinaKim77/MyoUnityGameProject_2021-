using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light theLight;
    [SerializeField] float minTimeBeforeLightFlickers;
    [SerializeField] float maxTimeBeforeLightFlickers;

    void Start()
    {
        theLight = GetComponent<Light>();

        StartCoroutine(MakeLightFlicker());
        //Or
        //StartCoroutine("MakeLightFlicker");
    }

    //yield return waitForSecond(1)
    IEnumerator MakeLightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBeforeLightFlickers, maxTimeBeforeLightFlickers));

            theLight.enabled = !theLight.enabled;
        }
    }
}
