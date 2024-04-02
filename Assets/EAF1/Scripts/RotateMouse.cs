using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateMouse : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private bool isRotating = false;
    private Vector2 previousMousePosition;

    void Update()
    {
        // Verificar si se ha hecho clic en el objeto
        if (Mouse.current.leftButton.wasPressedThisFrame && IsPointerOverGameObject())
        {
            isRotating = true;
            previousMousePosition = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isRotating = false;
        }

        // Rotar el objeto mientras se mantiene presionado el clic del rat�n
        if (isRotating && Mouse.current.leftButton.isPressed)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            float mouseX = previousMousePosition.x - currentMousePosition.x; // Invertir el movimiento del rat�n
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
            previousMousePosition = currentMousePosition;
        }
    }

    // M�todo para verificar si el puntero est� sobre el objeto
    private bool IsPointerOverGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        return Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject;
    }
}
