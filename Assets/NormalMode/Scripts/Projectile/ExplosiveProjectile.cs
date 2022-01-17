﻿using NormalMode.Enemies;

namespace NormalMode.Projectiles
{
    using UnityEngine;

    public class ExplosiveProjectile : Projectile
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
                if (collider.tag == "Enemy")
                {
                    if (CanAttack(collider.GetComponent<Enemy>()))
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
}

