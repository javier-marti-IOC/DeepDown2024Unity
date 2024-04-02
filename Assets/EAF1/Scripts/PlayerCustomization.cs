using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public static PlayerCustomization instance; // Singleton instance

    // Variables para la selecci�n del jugador
    private int selectedArmorIndex = 0;
    private int selectedWeaponIndex = 0;

    // Claves para PlayerPrefs
    private const string armorKey = "SelectedArmor";
    private const string weaponKey = "SelectedWeapon";

    // Referencia al script ModulesShaker
    public ModulesShaker modulesShaker;

    private void Awake()
    {
        // Configurar el singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para guardar la selecci�n del jugador
    public void SavePlayerSelection()
    {
        // Aqu� puedes proporcionar los �ndices de armadura y arma que deseas guardar
        // Suponiendo que tienes alguna l�gica para obtener los �ndices seleccionados
        int armorIndex = selectedArmorIndex;
        int weaponIndex = selectedWeaponIndex;

        PlayerPrefs.SetInt(armorKey, armorIndex);
        PlayerPrefs.SetInt(weaponKey, weaponIndex);
        PlayerPrefs.Save();
    }

    // M�todo para cargar la selecci�n del jugador al inicio de la escena
    public void LoadPlayerSelection()
    {
        if (PlayerPrefs.HasKey(armorKey) && PlayerPrefs.HasKey(weaponKey))
        {
            selectedArmorIndex = PlayerPrefs.GetInt(armorKey);
            selectedWeaponIndex = PlayerPrefs.GetInt(weaponKey);

            // Configurar el personaje principal con la selecci�n guardada
            // Llamar al m�todo SetAll del script ModulesShaker con la numeraci�n de la selecci�n guardada
            modulesShaker.SetAll(selectedArmorIndex.ToString("000"));
            // Opcionalmente, si deseas que la apariencia sea aleatoria al inicio, puedes comentar la l�nea anterior y descomentar la siguiente
            // modulesShaker.RandomizeAll();
        }
        else
        {
            // Si no se ha guardado ninguna selecci�n previa, utilizar valores predeterminados
            selectedArmorIndex = 0;
            selectedWeaponIndex = 0;
        }
    }
}
