using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar el objeto continuamente en el eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
