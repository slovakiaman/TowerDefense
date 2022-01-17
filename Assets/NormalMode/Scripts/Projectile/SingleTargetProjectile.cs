﻿namespace NormalMode.Projectiles
{
    public class SingleTargetProjectile : Projectile
    {
        protected override void InitializeProjectile()
        {
            //init
        }

        protected override void HitTarget()
        {
            showParticleEffects();

            Damage(enemyTarget);
            Destroy(gameObject);
        }

    }
}