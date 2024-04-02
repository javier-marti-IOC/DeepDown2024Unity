using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Controlador simple pel jugador. Les funcions de locomoció es troben configurades al ThirdPersonController així
 * que aquí només afegim les funcionalitats afegides (interactuar).
 */
public class PlayerController : MonoBehaviour
{
    private Interactor _interactor;
    
    void Start()
    {
        _interactor = GetComponentInChildren<Interactor>();
    }
    
    // Cridat pel InputSystem
    private void OnInteract()
    {
        _interactor.Interact();

    }

}
