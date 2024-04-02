using UnityEngine;

/**
 * Component que indica que un objecte te punts de vida
 */
public class Health : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnDeath;

    public delegate void OnHealthChangeDelegate(int amount);

    public event OnHealthChangeDelegate OnHealthChanged;


    [SerializeField] private int maxHealth = 50;
    private int _currentHealth;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private GameObject additionalDeathEffect; // Nuevo objeto a activar al morir
    private bool _isDead = false;

    public bool IsDead
    {
        get { return _isDead; }
    }

    public int MaxHealthValue
    {
        get { return maxHealth; }
    }

    protected virtual void Awake()
    {
        //_currentHealth = maxHealth;
        // Verificar si el objeto actual tiene la etiqueta "Player"
        if (gameObject.CompareTag("Player"))
        {
            // Recuperar la salud guardada del jugador desde la clase estática
            int savedHealth = PlayerStats.playerHealth;

            // Si la salud guardada es 0, inicializarla con el valor predeterminado
            if (savedHealth == 0)
            {
                _currentHealth = maxHealth;
            }
            else
            {
                _currentHealth = savedHealth;
            }
        }
        else
        {
            // Si el objeto no es el jugador, inicializar la salud con el valor predeterminado
            _currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, int.MaxValue);

        if (OnHealthChanged != null) OnHealthChanged(-damage);

        if (_currentHealth <= 0)
        {
            PlayEffect(deathEffect);
            Die();
        }
        else
        {
            PlayEffect(hitEffect);
        }
    }

    protected virtual void Die()
    {
        _isDead = true;
        if (OnDeath != null) OnDeath();

        if (gameObject.CompareTag("Player"))
        {
            float resetHealthDelay = 4.5f; // Retardo antes de restablecer la vida del jugador
            Invoke("ResetPlayerHealth", resetHealthDelay);
        }
        else
        {
            // Si no es el jugador, simplemente destruir el objeto después de un pequeño retardo
            float destroyDelay = 3.0f; // Ajusta este valor según sea necesario
            Invoke("DestroyObject", destroyDelay);
        }

        // Activar el nuevo efecto de muerte si está asignado
        if (additionalDeathEffect != null)
        {
            additionalDeathEffect.SetActive(true);
        }
    }

    // Método para destruir el objeto después de un retardo
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    void PlayEffect(ParticleSystem effect)
    {
        if (effect != null)
        {
            // Obtener la posición actual y añadir el desplazamiento en el eje Y
            Vector3 spawnPosition = transform.position + new Vector3(0f, 0.5f, 0f);
            ParticleSystem instance = Instantiate(effect, spawnPosition, Quaternion.identity);
            // Rotar el efecto de partículas en 90 grados en el eje X
            instance.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void Restore(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        if (OnHealthChanged != null) OnHealthChanged(amount);
    }
    public void RestoreToMaxHealth()
    {
        _currentHealth = maxHealth;
        if (OnHealthChanged != null) OnHealthChanged(maxHealth - _currentHealth);
    }

    private void OnDestroy()
    {
        // Guardar la salud actual del jugador en la clase estática antes de destruir el objeto
        PlayerStats.playerHealth = _currentHealth;
    }
    // Método para restablecer la vida del jugador
    private void ResetPlayerHealth()
    {
        _currentHealth = maxHealth;
    }
}