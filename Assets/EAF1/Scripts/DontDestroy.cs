using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {

        // Encuentra todos los objetos en la escena con el mismo nombre que este objeto
        GameObject[] objs = GameObject.FindGameObjectsWithTag(gameObject.tag);

        // Si hay más de un objeto con el mismo nombre, destruye este objeto
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Si este es el único objeto con su nombre, no lo destruyas en las cargas de escena
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
