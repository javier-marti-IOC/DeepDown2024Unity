using UnityEngine;

/**
 * Component que defineix la condició de victoria per aquest joc. Quan el jugador entra dins del vólum
 * el nivell es completa.
 */
[RequireComponent(typeof(Collider))]
public class VictoryVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Evitem que es dispari més d'una vegada
            GetComponent<Collider>().enabled = false;
            AudioManager.Instance.StopTrack();
            AudioManager.Instance.PlayVictoryClip();

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.EndLevel();
        }
    }
}