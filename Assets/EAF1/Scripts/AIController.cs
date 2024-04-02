using UnityEngine;
using UnityEngine.AI;

/**
 * Controlador general per la IA
 */
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AIAttackController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        Patrolling,
        Attacking,
        Following,
        Dead
    }

    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private GameObject[] patrolPoints; // Arreglo de GameObjects que representan los puntos de patrulla
    [Header("Sensors")] [SerializeField] private Collider reachCollider;
    [SerializeField] private Collider detectionCollider;

    private bool _alerted = false;

    private AIState _state;
    private AIState _previousState; // Estado anterior
    private Animator _animator;
    private NavMeshAgent _agent;

    private GameObject _target;
    private int _currentPatrolPointIndex = 0; // Índice del punto de patrulla actual

    private static readonly int _animIDSpeed = Animator.StringToHash("Speed");
    private static readonly int _animIDDead = Animator.StringToHash("Dead");

    private AIAttackController _aiAttackController;
    private Health _health;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _aiAttackController = GetComponent<AIAttackController>();
        _health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        reachCollider.GetComponent<TestTrigger>().OnTargetEnter += StartAttack;
        reachCollider.GetComponent<TestTrigger>().OnTargetExit += StartFollowing;

        detectionCollider.GetComponent<TestTrigger>().OnTargetEnter += Alert;
        detectionCollider.GetComponent<TestTrigger>().OnTargetExit += EndAlert; // Suscripción al evento OnTargetExit
        _previousState = _state; // Al inicio, guardamos el estado actual como estado anterior
        _health.OnDeath += ProcessDeath;

        // Convertir los GameObjects de los puntos de patrulla a transformaciones
        Transform[] transforms = new Transform[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            transforms[i] = patrolPoints[i].transform;
        }

        // Comienza la patrulla si hay puntos de patrulla definidos
        if (transforms.Length > 0)
        {
            _state = AIState.Patrolling;
            SetDestinationToNextPatrolPoint();
        }
    }

    private void ProcessDeath()
    {
        _state = AIState.Dead;
        _animator.SetTrigger(_animIDDead);
        Destroy(gameObject, 3f);
        Disable();

        GameState.Instance.DecreaseAlert();
    }

    private void Disable()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
        Collider characterCollider = GetComponent<Collider>();
        characterCollider.enabled = false;
        reachCollider.enabled = false;
        detectionCollider.enabled = false;
    }

    private void Update()
    {
        if (_state == AIState.Dead)
        {
            return;
        }

        UpdateMovement();
        UpdateAI();
    }

    private void UpdateMovement()
    {
        float speed = _agent.velocity.magnitude;
        _animator.SetFloat(_animIDSpeed, speed);

    }
    
    private void UpdateAI()
    {
        switch (_state)
        {
            case AIState.Idle:
                break;

            case AIState.Patrolling:
                if (_agent.remainingDistance < 0.1f)
                {
                    SetDestinationToNextPatrolPoint();
                }
                break;

            case AIState.Attacking:
                FaceTarget();
                _aiAttackController.StartAttack();
                _agent.isStopped = true;
                break;

            case AIState.Following:
                _agent.isStopped = false;
                _agent.SetDestination(_target.transform.position);
                _aiAttackController.StopAttack();
                break;
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void StartAttack(GameObject target)
    {
        _target = target;
        _state = AIState.Attacking;
    }

    void StartFollowing(GameObject target)
    {
        _target = target;
        _state = AIState.Following;
    }

    void Alert(GameObject target)
    {
        // Solo cambia al estado de seguimiento si no estás ya siguiendo a un objetivo
        if (_state != AIState.Following)
        {
            StartFollowing(target);

            if (!_alerted)
            {
                _alerted = true;
                GameState.Instance.IncreaseAlert();
            }
        }
    }
    void EndAlert(GameObject target) // Método para manejar la salida del jugador del collider de detección
    {
        // Cambiamos al estado anterior solo si ya no estamos en alerta (no hay más objetivos)
        if (_alerted)
        {
            _alerted = false;
            GameState.Instance.DecreaseAlert();

            // Si el estado anterior no era de ataque, cambiamos al estado de patrullaje
            if (_state != AIState.Attacking)
            {
                _state = AIState.Patrolling;
                SetDestinationToNextPatrolPoint();
            }
        }
    }
    private void SetDestinationToNextPatrolPoint()
    {
        Debug.Log("Setting destination to next patrol point");
        _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % patrolPoints.Length;
        _agent.SetDestination(patrolPoints[_currentPatrolPointIndex].transform.position);
    }
}