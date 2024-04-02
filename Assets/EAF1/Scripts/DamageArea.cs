using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float damagePerSecond = 10f;

    private Health playerHealth;
    private float lastDamageTime; // Guarda el tiempo del último daño aplicado

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("No se ha encontrado el componente Health en el jugador.");
        }
        lastDamageTime = Time.time; // Inicializa el tiempo del último daño
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float elapsedTime = Time.time - lastDamageTime; // Calcula el tiempo transcurrido desde el último daño
            if (elapsedTime >= 1f / damagePerSecond) // Aplica el daño si ha pasado el tiempo suficiente
            {
                int damageAmount = Mathf.RoundToInt(damagePerSecond); // Calcula el daño a infligir
                playerHealth.TakeDamage(damageAmount);
                AudioManager.Instance.PlayClip(impactSound, transform.position);
                lastDamageTime = Time.time; // Actualiza el tiempo del último daño
            }
        }
    }
}
