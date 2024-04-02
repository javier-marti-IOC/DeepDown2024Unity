using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }
    private Health playerHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Si ya hay una instancia, destruye este GameObject
            return;
        }

        Instance = this; // Establece esta instancia como la instancia única
        DontDestroyOnLoad(gameObject);

        // Obtener una referencia al componente Health del jugador al iniciar
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }

        // Suscribirse al evento sceneLoaded cuando este script se habilita
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Asegurarse de cancelar la suscripción al evento sceneLoaded cuando este script se destruye
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se llama cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Volver a obtener la referencia al componente Health del jugador si es necesario
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
            }
        }

        // Restaurar la salud del jugador si se encuentra
        if (playerHealth != null)
        {
            int savedHealth = PlayerPrefs.GetInt("PlayerHealth", playerHealth.MaxHealthValue);
            playerHealth.Restore(savedHealth - playerHealth.GetHealth());
        }
    }
}
