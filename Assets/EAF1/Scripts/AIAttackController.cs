using System.Collections;
using UnityEngine;

/**
 * Controlador d'atacs per la IA
 */
public class AIAttackController : AttackController
{
    public void StartAttack()
    {
        IsAttacking = true;
    }

    public void StopAttack()
    {
        IsAttacking = false;
    }

    public override void OnAnimationConnect()
    {
        GameObject[] targets = TestTrigger.GetTargets();

        if (targets.Length > 0)
        {
            Weapon.Attack(targets);
        }
    }
}