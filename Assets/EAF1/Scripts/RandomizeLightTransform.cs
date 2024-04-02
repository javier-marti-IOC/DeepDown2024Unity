using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeLightTransform : MonoBehaviour
{
    public float minIntensity = 1f;
    public float maxIntensity = 5f;
    public float minRange = 5f;
    public float maxRange = 10f;
    public float smoothness = 2f;

    private Light pointLight;
    private float targetIntensity;
    private float targetRange;

    void Start()
    {
        pointLight = GetComponent<Light>();

        if (pointLight == null)
        {
            Debug.LogError("El script requiere un componente Light adjunto al GameObject.");
            enabled = false;
            return;
        }

        targetIntensity = pointLight.intensity;
        targetRange = pointLight.range;
    }

    void Update()
    {
        // Llamar al método para cambiar suavemente la intensidad y el rango
        SmoothRandomizeLight();

        // Actualizar la posición del objeto de manera aleatoria
        Vector3 randomPosition = new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
        transform.position += randomPosition * Time.deltaTime;
    }

    void SmoothRandomizeLight()
    {
        if (Mathf.Approximately(pointLight.intensity, targetIntensity) && Mathf.Approximately(pointLight.range, targetRange))
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            targetRange = Random.Range(minRange, maxRange);
        }

        pointLight.intensity = Mathf.Lerp(pointLight.intensity, targetIntensity, Time.deltaTime * smoothness);
        pointLight.range = Mathf.Lerp(pointLight.range, targetRange, Time.deltaTime * smoothness);
    }
}
