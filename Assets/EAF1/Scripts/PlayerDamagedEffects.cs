using StarterAssets;
using UnityEngine;

/**
 * Component que reacciona al mal que pren el jugador i processa la seva mort.
 */
public class PlayerDamagedEffects : MonoBehaviour
{
    private Animator _animator;
    private int _animIDDead;
    private ThirdPersonController _locomotionController;
    private PlayerController _playerController;
    private PlayerAttackController _attackController;

    private bool _processedDeath;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animIDDead = Animator.StringToHash("Dead");

        _playerController = GetComponent<PlayerController>();
        _locomotionController = GetComponent<ThirdPersonController>();
        _attackController = GetComponent<PlayerAttackController>();
        GetComponent<Health>().OnDeath += OnDeath;
        GetComponent<Health>().OnHealthChanged += OnHealthChanged;
    }


    private void OnHealthChanged(int amount)
    {
        // TODO: Exercici 3. Quan el jugador perd punts de vida ha de tremolar la càmera
        // Obtener la intensidad y duración del temblor de cámara
        float intensity = 2f; // Intensidad del temblor
        float duration = 0.5f; // Duración del temblor

        // Llamar al método Shake de CinemachineShake
        CinemachineShake.Instance.Shake(intensity, duration);
    }

    private void OnDeath()
    {
        if (_processedDeath)
        {
            return;
        }

        _processedDeath = true;
        _animator.SetTrigger(_animIDDead);
        _playerController.enabled = false;
        _locomotionController.enabled = false;
        _attackController.enabled = false;
        NotificationManager.Instance.ShowNotification("You have been defeated!");
        GameManager.LoadGameOver();
        AudioManager.Instance.PlayDefeatClip();
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDeath -= OnDeath;
        GetComponent<Health>().OnHealthChanged -= OnHealthChanged;
    }
}