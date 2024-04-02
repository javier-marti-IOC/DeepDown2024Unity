using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Component per configurar la interficie d'usuari durant el joc.
 */
public class UIDisplay : MonoBehaviour
{
    [Header("Player Stats")] [SerializeField]
    private Slider healthSlider;

    [SerializeField] private Health playerHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private LevelManager _levelManager;

    [Header("Animations")] [SerializeField]
    Animation scoreAnimation;

    [SerializeField] Animation healthAnimation;

    void Start()
    {
        GameState.Instance.OnScoreChanged += UpdateScore;

        _levelManager = FindObjectOfType<LevelManager>();
        levelText.text = _levelManager.GetCurrentLevelTitle();

        healthSlider.maxValue = playerHealth.MaxHealthValue;
        healthSlider.value = playerHealth.GetHealth();

        scoreText.text = GameState.Instance.GetScore().ToString();

        playerHealth.OnHealthChanged += UpdateHealth;
    }

    void UpdateHealth(int amount)
    {
        int newHealth = playerHealth.GetHealth();

        healthSlider.value = newHealth;

        if (amount < 0 && healthAnimation)
        {
            healthAnimation.Play();
        }
    }

    void UpdateScore()
    {
        if (scoreAnimation)
        {
            scoreAnimation.Play();
        }

        scoreText.text = GameState.Instance.GetScore().ToString();
    }
    
    private void OnDestroy()
    {
        // Com que _gameState és un singleton, si no ens desuscribim quan
        // es canvia de nivell continua intentant actualitzar la UI antiga
        GameState.Instance.OnScoreChanged -= UpdateScore;
    }
}