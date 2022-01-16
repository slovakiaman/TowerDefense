using UnityEngine;

public class EndlessAOEAttackTower : EndlessTower
{
    private Transform lastNearbyEnemy;
    protected override void InitializeTower()
    {
        if (damage == 0)
            Debug.LogError("Damage too low");
    }

    protected override void UpdateTower()
    {
        SetShootingAnimationState(false);
        if (!isLastNearbyEnemyInRange())
            SearchForEnemies();
        Attack();
    }

    protected override void Attack()
    {
        if (lastNearbyEnemy == null)
            return;

        if (!EndlessValueCalculator.instance.IsTowerEnabled(this))
            return;
        
        SetShootingAnimationState(true);
        if (reloadTimer <= 0)
        {
            reloadTimer = 1 / EndlessValueCalculator.instance.CalculateTowerSPEED(this);
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    if (CanAttack(collider.GetComponent<EndlessEnemy>()))
                        Damage(collider.transform);
                }
            }
            ShowParticles();
        }
    }

    protected override void ReloadUpdate()
    {
        reloadTimer -= Time.deltaTime;
        if (lastNearbyEnemy == null)
        {
            reloadTimer = reloadTimer < inactiveTreshold ? inactiveTreshold : reloadTimer;
        }
    }

    private bool isLastNearbyEnemyInRange()
    {
        if (lastNearbyEnemy == null)
            return false;
        return Vector3.Distance(lastNearbyEnemy.position, transform.position) < range;
    }

    private void SearchForEnemies()
    {
        lastNearbyEnemy = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                lastNearbyEnemy = collider.transform;
                return;
            }
        }
    }

    private void Damage(Transform enemy)
    {
        EndlessEnemy e = enemy.GetComponent<EndlessEnemy>();
        if (e != null)
        {
            int calculatedDamage = EndlessValueCalculator.instance.CalculateTowerDMG(this);
            e.TakeDamage(calculatedDamage);
        }
    }

    private void SetShootingAnimationState(bool shoot)
    {
        if (controller != null)
            controller.SetBool("isShooting", shoot);
    }

    public void PlaySound()
    {
        shootSound.Play();
    }
}
