using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealthTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Referencia al AudioSource que contiene el sonido a reproducir

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador ha entrado en el área del trigger
        if (other.CompareTag("Player"))
        {
            // Obtener el componente Health del jugador
            Health playerHealth = other.GetComponent<Health>();

            // Verificar si se encontró el componente Health
            if (playerHealth != null)
            {
                // Restaurar la salud del jugador a su valor máximo
                playerHealth.RestoreToMaxHealth();
            }
            else
            {
                Debug.LogWarning("Player Health component not found!");
            }

            // Verificar si hay un AudioSource y está configurado para reproducir
            if (audioSource != null && audioSource.clip != null)
            {
                // Reproducir el sonido asociado al AudioSource
                audioSource.Play();
            }
        }
    }
}
