using UnityEngine;

/**
 * Component auxiliar per solucionar un problema amb el cursor, ja que aquest ha de ser visible o no
 * segons si el jugador es troba a una escena de menus o dins del joc.
 */
public class UnlockCursor : MonoBehaviour
{
    [SerializeField] bool unlockCursor = true;

    private void Start()
    {
        Cursor.lockState = unlockCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = unlockCursor;
    }
}