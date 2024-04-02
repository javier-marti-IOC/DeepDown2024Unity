using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Requirements/Score", fileName = "New Requirement Score")]
public class RequirementScoreSO : RequirementSO
{
    [SerializeField] private int requiredScore;

    public override bool Validate(GameObject gameObject)
    {
        GameState gameState = GameState.Instance;

        if (gameState == null)
        {
            Debug.LogError("GameState instance not found.");
            return false;
        }

        return gameState.GetScore() >= requiredScore;
    }

    public override string GetErrorMessage()
    {
        return $"Score required: {requiredScore}";
    }
}
