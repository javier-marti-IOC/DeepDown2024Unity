using TMPro;
using UnityEngine;

/**
 * Component simple per actualitzar la interficie de Game Over i Victoria
 */
public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;


    void Start()
    {
        scoreText.text = "Score: " + GameState.Instance.GetScore();
    }
}