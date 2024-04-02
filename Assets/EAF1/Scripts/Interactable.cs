using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Component pels elements amb els que es pot interactuar, per exemple portes i cofres
 */
public class Interactable : MonoBehaviour
{
    public UnityEvent OnSelect;
    public UnityEvent OnUnselect;
    public UnityEvent OnInteract;
    public UnityEvent OnRequirementFail;

    [SerializeField] private List<RequirementSO> requirements;

    public void Select(GameObject selector)
    {
        OnSelect.Invoke();
    }

    public void Unselect(GameObject selector)
    {
        OnUnselect.Invoke();
    }


    public bool Interact(GameObject interactor)
    {
        if (ValidateRequirements(interactor))
        {
            OnInteract.Invoke();
            return true;
        }
        else
        {
            OnRequirementFail.Invoke();
            return false;
        }
    }

    private bool ValidateRequirements(GameObject gameObject)
    {
        foreach (RequirementSO requirement in requirements)
        {
            if (!requirement.Validate(gameObject))
            {
                NotificationManager.Instance.ShowNotification(requirement.GetErrorMessage());
                return false;
            }
        }

        return true;
    }
}