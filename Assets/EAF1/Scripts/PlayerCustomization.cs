using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public static PlayerCustomization instance; // Singleton instance

    // Variables para la selección del jugador
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

    // Método para guardar la selección del jugador
    public void SavePlayerSelection()
    {
        // Aquí puedes proporcionar los índices de armadura y arma que deseas guardar
        // Suponiendo que tienes alguna lógica para obtener los índices seleccionados
        int armorIndex = selectedArmorIndex;
        int weaponIndex = selectedWeaponIndex;

        PlayerPrefs.SetInt(armorKey, armorIndex);
        PlayerPrefs.SetInt(weaponKey, weaponIndex);
        PlayerPrefs.Save();
    }

    // Método para cargar la selección del jugador al inicio de la escena
    public void LoadPlayerSelection()
    {
        if (PlayerPrefs.HasKey(armorKey) && PlayerPrefs.HasKey(weaponKey))
        {
            selectedArmorIndex = PlayerPrefs.GetInt(armorKey);
            selectedWeaponIndex = PlayerPrefs.GetInt(weaponKey);

            // Configurar el personaje principal con la selección guardada
            // Llamar al método SetAll del script ModulesShaker con la numeración de la selección guardada
            modulesShaker.SetAll(selectedArmorIndex.ToString("000"));
            // Opcionalmente, si deseas que la apariencia sea aleatoria al inicio, puedes comentar la línea anterior y descomentar la siguiente
            // modulesShaker.RandomizeAll();
        }
        else
        {
            // Si no se ha guardado ninguna selección previa, utilizar valores predeterminados
            selectedArmorIndex = 0;
            selectedWeaponIndex = 0;
        }
    }
}
