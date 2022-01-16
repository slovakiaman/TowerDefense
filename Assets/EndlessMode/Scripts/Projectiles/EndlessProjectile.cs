using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class EndlessProjectile : MonoBehaviour
{
    [Header("Projectile stats")]
    public float speed = 70f;
    public int damage = 20;
    [SerializeField] protected List<WeakAgainstType> weakAgainst = new List<WeakAgainstType>();

    public GameObject particleEffect;
    protected Transform enemyTarget;

    void Start()
    {
        InitializeProjectile();
    }

    void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = enemyTarget.position - transform.position;
        float distanceToTarget = direction.magnitude;
        float distanceThisFrame = speed * Time.deltaTime;

        if (distanceToTarget <= distanceThisFrame)
        {
            HitTarget();
        }
        else
        {
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            transform.LookAt(enemyTarget);
        }
    }

    public void SetEnemyTarget(Transform target)
    {
        enemyTarget = target;
    }

    public void SetWeakAgainst(List<WeakAgainstType> enemyTypes)
    {
        this.weakAgainst = enemyTypes;
    }

    protected abstract void InitializeProjectile();

    protected abstract void HitTarget();

    protected void Damage(Transform enemy) 
    {
        EndlessEnemy e = enemy.GetComponent<EndlessEnemy>();
        if (e != null)
            e.TakeDamage(damage);
    }

    protected void showParticleEffects()
    {
        if (particleEffect != null)
        {
            GameObject particleEffectObject = (GameObject)Instantiate(particleEffect, transform.position, transform.rotation);
            ParticleSystem parts = particleEffectObject.GetComponent<ParticleSystem>();
            float totalDuration = parts.main.duration;
            Destroy(particleEffectObject, totalDuration);
        }
    }

    public bool CanAttack(EndlessEnemy enemy)
    {
        return !weakAgainst.Contains(enemy.weakAgainstType);
    }

}
