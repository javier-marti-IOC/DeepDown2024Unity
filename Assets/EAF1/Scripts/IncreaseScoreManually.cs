using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseScoreManually : MonoBehaviour
{
    [SerializeField] private int scoreIncreaseAmount = 100;

    public void IncreaseScore()
    {
        GameState.Instance.IncreaseScore(scoreIncreaseAmount);
    }
}
