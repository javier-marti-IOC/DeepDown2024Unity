using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float damagePerSecond = 10f;

    private Health playerHealth;
    private float lastDamageTime; // Guarda el tiempo del �ltimo da�o aplicado

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("No se ha encontrado el componente Health en el jugador.");
        }
        lastDamageTime = Time.time; // Inicializa el tiempo del �ltimo da�o
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float elapsedTime = Time.time - lastDamageTime; // Calcula el tiempo transcurrido desde el �ltimo da�o
            if (elapsedTime >= 1f / damagePerSecond) // Aplica el da�o si ha pasado el tiempo suficiente
            {
                int damageAmount = Mathf.RoundToInt(damagePerSecond); // Calcula el da�o a infligir
                playerHealth.TakeDamage(damageAmount);
                AudioManager.Instance.PlayClip(impactSound, transform.position);
                lastDamageTime = Time.time; // Actualiza el tiempo del �ltimo da�o
            }
        }
    }
}
