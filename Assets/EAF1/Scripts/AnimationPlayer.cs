using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component auxiliar que permet repdrouir animacions independents sense requerir un animator
 */
public class AnimationPlayer : MonoBehaviour
{
    private Animation _animation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }


    public void Play()
    {
        _animation.Play();
    }
}