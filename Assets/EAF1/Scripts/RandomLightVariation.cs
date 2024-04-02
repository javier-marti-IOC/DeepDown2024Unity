using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightVariation : MonoBehaviour
{
    public float minIntensity = 1f;
    public float maxIntensity = 5f;
    public float minRange = 5f;
    public float maxRange = 10f;
    public float smoothness = 2f; // Ajusta este valor para cambiar la suavidad del cambio

    private Light pointLight;
    private float targetIntensity;
    private float targetRange;

    void Start()
    {
        // Obtener el componente Light adjunto al GameObject
        pointLight = GetComponent<Light>();

        // Comprobar si el componente Light existe
        if (pointLight == null)
        {
            Debug.LogError("El script requiere un componente Light adjunto al GameObject.");
            enabled = false;  // Desactivar el script si no hay componente Light
            return;
        }

        // Inicializar los valores de destino con los valores iniciales de la luz
        targetIntensity = pointLight.intensity;
        targetRange = pointLight.range;
    }

    void Update()
    {
        // Llamar al método para cambiar suavemente la intensidad y el rango
        SmoothRandomizeLight();
    }

    void SmoothRandomizeLight()
    {
        // Si hemos alcanzado el objetivo, generar nuevos valores aleatorios
        if (Mathf.Approximately(pointLight.intensity, targetIntensity) && Mathf.Approximately(pointLight.range, targetRange))
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            targetRange = Random.Range(minRange, maxRange);
        }

        // Suavizar el cambio con Lerp
        pointLight.intensity = Mathf.Lerp(pointLight.intensity, targetIntensity, Time.deltaTime * smoothness);
        pointLight.range = Mathf.Lerp(pointLight.range, targetRange, Time.deltaTime * smoothness);
    }
}
