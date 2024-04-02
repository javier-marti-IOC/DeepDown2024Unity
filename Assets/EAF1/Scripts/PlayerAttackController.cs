using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Controlador d'atacs pel jugador.
 */
public class PlayerAttackController : AttackController
{

    // Disparat pel InputSystem
    public void OnAttack(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public override void OnAnimationConnect()
    {
        GameObject[] detectedTargets = TestTrigger.GetTargets();
        List<GameObject> targets = new List<GameObject>();
        // Descartem els enemics que siguin morts

        foreach (GameObject target in detectedTargets)
        {
            if (!target)
            {
                TestTrigger.RemoveTarget(target);
                continue;
            }

            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null && !targetHealth.IsDead)
            {
                targets.Add(target);
            }
            else
            {
                // El _testTrigger no controla si un objectiu és mort o no, s'ho comuniquem per descartar-lo
                TestTrigger.RemoveTarget(target);
            }
        }


        if (targets.Count > 0)
        {
            CinemachineShake.Instance.Shake(0.8f, 0.2f);
            Weapon.Attack(targets.ToArray());
        }
    }
}