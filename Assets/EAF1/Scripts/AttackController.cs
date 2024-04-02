using System.Collections;
using UnityEngine;


/**
 * Controlador d'atacs basat en events llençat per animacions i que permet l'atac continu.
 * 
 * Les subclasses són les responsables de determinar que passa quan la animació llença l'event
 * OnAnimationConnect.
 */
abstract public class AttackController : MonoBehaviour, IAnimationCycle
{
    protected Animator Animator;
    protected Weapon Weapon;
    protected TestTrigger TestTrigger;

    [SerializeField] protected float attackCooldown = 0.5f;
    [SerializeField] protected Collider attackCollider;

    protected bool IsAttacking = false;
    private Coroutine _attackingCoroutine;


    // animation IDs
    private int _animIDAttacking;
    
    protected void Start()
    {
        if (attackCollider != null)
        {
            TestTrigger = attackCollider.GetComponent<TestTrigger>();
        }
        
        TryGetComponent(out Animator);
        _animIDAttacking = Animator.StringToHash("Attacking");

        Weapon = GetComponentInChildren<Weapon>();
    }

    protected void Update()
    {
        if (IsAttacking && _attackingCoroutine == null)
        {
            _attackingCoroutine = StartCoroutine(AttackContinuously());
        }
        else if (!IsAttacking && _attackingCoroutine != null)
        {
            StopCoroutine(_attackingCoroutine);
            _attackingCoroutine = null;
        }
    }

    IEnumerator AttackContinuously()
    {
        while (true)
        {
            Animator.SetTrigger(_animIDAttacking);
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    public abstract void OnAnimationConnect();

    public void OnAnimationStart()
    {
        Weapon.StartUsing();
    }

    public void OnAnimationEnd()
    {
        Weapon.StopUsing();
    }
}