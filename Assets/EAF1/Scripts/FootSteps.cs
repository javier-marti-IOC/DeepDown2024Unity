using UnityEngine;

/**
 * Component per sincronitzar els sons de pitjades i salts (s'han de cridar a les funcions des de events d'animació)
 */
public class FootSteps : MonoBehaviour
{
    [Header("SFX")] [SerializeField] AudioClip[] FootStepsClip;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] AudioClip LandingSound;


    private int _currentStep = 0;

    public void PlayFootSteps()
    {
        // TODO: Exercici 4. Reproduir el clip de l'array FootStepsClip utilitzant el AudioManager,
        // incrementar el comptador _currentStep i si supera el nombre de clips posar-lo a 0
        // Nota: El so ha de reproduir-se a la posició del personatge
        if (FootStepsClip.Length == 0) return;

        // Obtener la posición del gameobject que lleva este script
        Vector3 position = transform.position;

        // Reproducir el clip actual en la posición del gameobject
        AudioManager.Instance.PlayClip(FootStepsClip[_currentStep], position);

        // Incrementar el contador de pasos y volver a 0 si supera la cantidad de clips
        _currentStep = (_currentStep + 1) % FootStepsClip.Length;

    }

    private void JumpStart()
    {
        // TODO: Exercici 4. Reproduir el clip corresponent a JumpSound
        // Reproducir el clip correspondiente a JumpSound
        AudioManager.Instance.PlayClip(JumpSound, transform.position);
    }

    private void JumpEnd()
    {
        // TODO: Exercici 4. Reproduir el clip corresponent a JumpEnd
        // Reproducir el clip correspondiente a LandingSound
        AudioManager.Instance.PlayClip(LandingSound, transform.position);
    }
}