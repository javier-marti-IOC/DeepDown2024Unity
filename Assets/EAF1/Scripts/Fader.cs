using UnityEngine;

/**
 * Compoment per fer fade in i fade out de la pantalla
 */
public class Fader : MonoBehaviour
{
    [SerializeField] Animation fadeAnimation;
    [SerializeField] AnimationClip fadeOutAnimationClip;
    [SerializeField] AnimationClip fadeInAnimationClip;


    private void Start()
    {
        GameManager.Instance.OnLevelChange += FadeOut;
        FadeIn();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelChange -= FadeOut;
    }

    private void FadeOut()
    {
        fadeAnimation.clip = fadeOutAnimationClip;
        fadeAnimation.Play();
    }

    private void FadeIn()
    {
        fadeAnimation.clip = fadeInAnimationClip;
        fadeAnimation.Play();
    }
}