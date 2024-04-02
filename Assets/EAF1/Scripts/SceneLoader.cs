using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private PlayerCustomization playerCustomization;

    private void Start()
    {
        playerCustomization = PlayerCustomization.instance;
        if (playerCustomization != null)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cargar la selección del jugador al iniciar una nueva escena
        playerCustomization.LoadPlayerSelection();
    }

    private void OnDestroy()
    {
        if (playerCustomization != null)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
