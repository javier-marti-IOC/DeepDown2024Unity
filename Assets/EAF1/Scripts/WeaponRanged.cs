using UnityEngine;

/**
 * Component que instancia un projectil en atacar en joc de causar mal directament. El projectil serà el
 * responsable d'aplicar el mal.
 */
public class WeaponRanged : Weapon
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    public override void Attack(GameObject[] targets)
    {
        // Només processem un objectiu, això només ho fan servir els enemics. Si fos necessari pel jugador
        // S'hauria de fer servir una altra classe per adquirir un único objectiu (per exemple, el seleccionat 
        // fent clic o el més proper en la direcció que mira el jugador.

        if (targets.Length == 0)
        {
            // Aquest cas no s'ha de donar mai
            return;
        }

        GameObject target = targets[0];

        Vector3 spawnPosition = spawnPoint.position;
        Vector3 targetPosition = target.transform.position;


        Vector3 relativePosition = targetPosition - transform.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition,
            Quaternion.LookRotation(relativePosition, Vector3.up));

        // No passem cap dada al projectil, per simplificar el mal l'aplicarà el projectil en impactar.
        
    }
}