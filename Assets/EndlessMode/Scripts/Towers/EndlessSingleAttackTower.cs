using UnityEngine;

public class EndlessSingleAttackTower : EndlessTower
{
    private Transform enemyTarget;
    private RotatingObject rotatingParts;
    public GameObject bulletPrefab;
    public Transform projectileSpawnPoint;

    protected override void InitializeTower()
    {
        rotatingParts = gameObject.GetComponent<RotatingObject>();
        if (bulletPrefab == null)
            Debug.LogError("Tower missing bullet prefab");
    }

    protected override void UpdateTower()
    {
        UpdateTarget();
        Rotate();
        Attack();
    }

    private void UpdateTarget()
    {
        SetShootingAnimationState(false);

        if (enemyTarget != null && (Vector3.Distance(enemyTarget.position, transform.position) > range || enemyTarget.CompareTag("Dead")))
            enemyTarget = null;

        if (enemyTarget != null)
            return;

        if (!EndlessValueCalculator.instance.IsTowerEnabled(this))
            return;            

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (CanAttack(enemy.GetComponent<EndlessEnemy>()))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < minDistance)
                {
                    minDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null && minDistance <= range)
        {
            enemyTarget = closestEnemy.transform;
        }
        else
        {
            enemyTarget = null;
            SetShootingAnimationState(false);
        }
    }

    protected override void Attack()
    {
        if (enemyTarget != null)
        {
            if (reloadTimer <= 0)
            {
                reloadTimer = 1 / EndlessValueCalculator.instance.CalculateTowerSPEED(this);
                GameObject projectileObject = (GameObject)Instantiate(bulletPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                projectileObject.GetComponent<EndlessProjectile>().damage = EndlessValueCalculator.instance.CalculateTowerDMG(this);
                EndlessProjectile projectile = projectileObject.GetComponent<EndlessProjectile>();
                projectile.SetWeakAgainst(this.weakAgainst);
                if (projectile != null)
                {
                    SetShootingAnimationState(true);
                    projectile.SetEnemyTarget(enemyTarget);
                }
                ShowParticles();
                shootSound.Play();
            }
        }
    }

    protected override void ReloadUpdate()
    {
        reloadTimer -= Time.deltaTime;
        if (enemyTarget == null)
        {
            reloadTimer = reloadTimer < inactiveTreshold ? inactiveTreshold : reloadTimer;
        }
    }

    private void SetShootingAnimationState(bool shoot)
    {
        
        if (controller != null)
            controller.SetBool("isShooting", shoot);
    }

    private void Rotate()
    {
        if (rotatingParts != null && enemyTarget != null)
            rotatingParts.RotateToObject(enemyTarget);
    }

}
