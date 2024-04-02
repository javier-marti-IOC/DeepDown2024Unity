using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Component simple que incrementa la puntuació del jugador quan l'element que inclou aquest
 * component mor.
 */
public class Score : MonoBehaviour
{
    [SerializeField] private int score = 10;


    private void Start()
    {
        GetComponent<Health>().OnDeath += IncreaseScore;
    }

    private void IncreaseScore()
    {
        GameState.Instance.IncreaseScore(score);
    }

    private void OnDestroy()
    {
        GetComponent<Health>().OnDeath -= IncreaseScore;
    }
}