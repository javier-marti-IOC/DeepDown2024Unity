using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Animator _animator;
    private bool _invulnerable = false;
    private PlayerAttackController _attackController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _attackController = GetComponent<PlayerAttackController>();
    }

    public void OnAnimationEventStartInvulnerability()
    {
        // Activar el estado de invulnerabilidad
        _invulnerable = true;
        // Tambi�n podr�as agregar aqu� cualquier otra l�gica que necesites cuando comience la invulnerabilidad
        // Desactivar la capa de ataque mientras el personaje est� rodando
        // Desactivar el componente PlayerAttackController
        if (_attackController != null)
        {
            _attackController.enabled = false;
        }
        _animator.SetLayerWeight(_animator.GetLayerIndex("Attack Layer"), 0f);
    }

    public void OnAnimationEventEndInvulnerability()
    {
        // Desactivar el estado de invulnerabilidad
        _invulnerable = false;
        // Tambi�n podr�as agregar aqu� cualquier otra l�gica que necesites cuando termine la invulnerabilidad
        // Activa la capa de ataque nuevamente cuando termina el rodar
        // Activar el componente PlayerAttackController
        if (_attackController != null)
        {
            _attackController.enabled = true;
        }
        _animator.SetLayerWeight(_animator.GetLayerIndex("Attack Layer"), 1f);
    }

    public bool IsInvulnerable()
    {
        return _invulnerable;
    }
}
