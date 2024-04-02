using UnityEngine;

/**
 * Component responsable d'activar i desactivar els efectes de l'arma i aplicar el mal a l'adversari.
 */
public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    [SerializeField] private AudioClip swingSound;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private GameObject impactVFX;

    [SerializeField] private int damage;

    private void Start()
    {
        if (trail != null)
        {
            trail.SetActive(false);
        }
    }

    public void StartUsing()
    {
        if (trail)
        {
            trail.SetActive(true);
        }

        AudioManager.Instance.PlayClip(swingSound, transform.position);
    }

    virtual public void Attack(GameObject[] targets)
    {
        AudioManager.Instance.PlayClip(impactSound, transform.position);

        // Calculem el punt d'impacte per cada objectiu
        Vector3 weaponPos = gameObject.transform.position;

        foreach (GameObject target in targets)
        {
            // Obtener el componente PlayerAnimationEvents del objetivo
            PlayerAnimationEvents playerAnimationEvents = target.GetComponent<PlayerAnimationEvents>();

            if (playerAnimationEvents != null && playerAnimationEvents.IsInvulnerable())
            {
                // Si el jugador está en estado de invulnerabilidad, el ataque falla
                Debug.Log("El enemigo está en estado de invulnerabilidad. El ataque ha fallado.");
                continue; // Saltar a la siguiente iteración del bucle
            }

            Animator targetAnimator = target.GetComponent<Animator>();
            // La posició dels personatges es calcula des dels peus, afegim com offset la meitat de la mida del
            // component perque es trobi al centre
            float heightOffset = target.GetComponent<Collider>().bounds.size.y / 2;

            // Calculem el punt de colisió més proper al arma
            Vector3 targetPosition = target.transform.position;
            targetPosition.y += heightOffset;

            // Ara podem calcular la direcció normalitzada (vector amb módul 1)
            Vector3 dir = (targetPosition - weaponPos).normalized;

            // Fem un raycast capturant tots els elements a la ruta, ja que si no els enemics al davant bloquejarien
            // als enemics del darrere
            RaycastHit[] hits;
            hits = Physics.RaycastAll(weaponPos, dir, 5f);
            Debug.DrawRay(weaponPos, dir, Color.green, 2f);

            Vector3 impactPosition = target.transform.position;
            Vector3 impactNormal = new Vector3(0f, 0f, 0f);

            // Cerquem el gameobject entre els impactes i asignem els valors correspondents a l'impacte i la normal
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == target)
                {
                    impactPosition = hit.point;
                    impactNormal = hit.normal;
                    break;
                }
            }

            // Desplacem la posició de l'impacte cap al jugador 0.1 unitats perque es trobi fora de l'enemic
            if (impactVFX != null && !playerAnimationEvents.IsInvulnerable())
            {
                var FX = Instantiate(impactVFX, impactPosition - (dir * 0.1f),
                    Quaternion.FromToRotation(Vector3.up, impactNormal));
                Destroy(FX, 2f);
            }

            // Realizar el daño solo si el jugador no está en estado de invulnerabilidad
            if (!playerAnimationEvents.IsInvulnerable())
            {
                target.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }


    public void StopUsing()
    {
        if (trail)
        {
            trail.SetActive(false);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}