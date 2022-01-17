namespace EndlessMode.Projectiles
{
 
    public class SingleTargetProjectile : Projectile
    {
        protected override void InitializeProjectile()
        {
            //init
        }

        protected override void HitTarget()
        {
            ShowParticleEffects();

            Damage(enemyTarget);
            Destroy(gameObject);
        }
    
    }   
}
