using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;

    public void SetActiveObjects(bool isActive)
    {
        if (object1 != null)
        {
            object1.SetActive(isActive);
        }

        if (object2 != null)
        {
            object2.SetActive(isActive);
        }
    }
}
