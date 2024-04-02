using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;

/**
 * Component per definir un projectil que es mou en línia recta i causa mal en impactar contra un objectiu
 * que tingui el component Health. Es discrimina entre jugadors i enemics pel layer al que s'ha assignat el
 * projectil.
 */
public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private GameObject impactVfxPrefab;
    [SerializeField] private AudioClip impactSound;

    [SerializeField] private float speed;
    [SerializeField] private int damage = 10;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacte:" + other);

        AudioManager.Instance.PlayClip(impactSound);

        GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
        Destroy(FX, 2f);

        GameObject additionalFX = Instantiate(impactVfxPrefab, transform.position, transform.rotation);
        Destroy(additionalFX, 2f);

        Health health = other.gameObject.GetComponent<Health>();

        // Si te salut apliquem el mal
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}