using UnityEngine;

/**
 * Component simple per forçar al AudioManager a reproduir una pista quan no hi ha cap configurada (als menus).
 */
public class UnmanagedMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    void Start()
    {
        AudioManager.Instance.PlayTrack(music);
    }
}