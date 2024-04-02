using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component simple que permet a altres elements cridar al mètode AttToPlayerInventory (per exemple, un interactables)
 * per afegir tots els items configurats a l'inventari del jugador.
 * 
 */
public class Loot : MonoBehaviour
{
    [SerializeField] List<String> loot = new List<string>();

    private Inventory _inventory;

    private void Start()
    {
        // Com només el jugador té inventary podem cercar-lo directament
        _inventory = FindObjectOfType<Inventory>();
    }

    public void AddToPlayerInventory()
    {
        foreach (String item in loot)
        {
            _inventory.Add(item);
            Debug.Log($"Added {item} in inventory");
        }

        loot.Clear();
    }
}