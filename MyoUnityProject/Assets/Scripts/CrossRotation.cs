using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRotation : MonoBehaviour
{
    public float rotationSpeed = -40.0f;

    private void Update() {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
