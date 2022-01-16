using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessExplosiveProjectile : EndlessProjectile
{
    public float explosionRadius = 0f;

    protected override void InitializeProjectile()
    {
        //init
    }

    protected override void HitTarget()
    {
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        showParticleEffects();
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if (CanAttack(collider.GetComponent<EndlessEnemy>()))
                    Damage(collider.transform);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
