using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttackTower : Tower
{
    private Transform lastNearbyEnemy;
    [SerializeField]
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
        SetShootingAnimationState(true);
        if (reloadTimer <= 0)
        {
            reloadTimer = 1 / fireRate;
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    if (CanAttack(collider.GetComponent<Enemy>()))
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
            return;
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
            if (collider.tag == "Enemy")
            {
                lastNearbyEnemy = collider.transform;
                return;
            }
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
            e.TakeDamage(damage);
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
