using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Gestor de la càrrega d'escenes al joc
 */
public class GameManager : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnLevelChange;


    [Header("Menus")] [SerializeField] private string mainMenu = "MainMenu";
    [SerializeField] private string gameOver = "GameOver";
    [SerializeField] private string victory = "Victory";

    [Header("Load Delays")] [SerializeField]
    private float sceneLoadDelay = 2f;

    [SerializeField] private float sceneDefeatDelay = 5f;
    [SerializeField] private float sceneVictoryDelay = 5f;


    [Header("Levels")] [SerializeField] private GameLevelsSO gameLevels;


    public static GameLevelsSO GameLevels
    {
        get { return _instance.gameLevels; }
    }


    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static void LoadGame()
    {
        GameState.Instance.Reset();
        var level = Instance.gameLevels.Levels[0];

        // Restablecer la vida del jugador al iniciar el juego
        PlayerStats.playerHealth = 50; // O el valor predeterminado deseado

        LoadLevel(level.sceneName);
    }

    public static void LoadMainMenu()
    {
        LoadLevel(Instance.mainMenu);
    }

    public static void LoadGameOver()
    {
        AudioManager.Instance.StopTrack();
        Instance.DelayedLoadLevel(Instance.gameOver, Instance.sceneDefeatDelay);
    }

    public static void LoadVictory()
    {
        AudioManager.Instance.StopTrack();
        Instance.DelayedLoadLevel(Instance.victory, Instance.sceneVictoryDelay);
    }


    public static void LoadLevel(string sceneName)
    {
        Instance.DelayedLoadLevel(sceneName, Instance.sceneLoadDelay);
    }

    private void DelayedLoadLevel(string sceneName, float delay)
    {
        if (OnLevelChange != null)
            OnLevelChange();

        // Buscar el jugador en la escena
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // Obtener la referencia al script de salud del jugador
            Health playerHealthScript = playerObject.GetComponent<Health>();

            if (playerHealthScript != null)
            {
                // Guardar la salud actual del jugador en la clase estática antes de cambiar de escena
                PlayerStats.playerHealth = playerHealthScript.GetHealth();
            }
        }

        // Invocar la carga de escena después del retraso especificado
        Invoke("LoadSceneWithDelay", delay);

        // Almacenar el nombre de la escena en una variable temporal para que esté disponible en el método auxiliar
        sceneToLoad = sceneName;
    }

    // Función para cargar la escena después del retraso
    // Método auxiliar para cargar la escena después del retraso
    private string sceneToLoad; // Variable para almacenar el nombre de la escena
    private void LoadSceneWithDelay()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
}