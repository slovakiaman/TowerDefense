using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessChainProjectile : EndlessProjectile
{
    private int numberOfChains = 3;
    [SerializeField]
    private GameObject chainPrefab;

    protected override void HitTarget()
    {
        showParticleEffects();
        List<Transform> enemiesFound = new List<Transform>();
        FindEnemies(numberOfChains, enemyTarget, new HashSet<Transform>(), enemiesFound);
        ChainEnemies(enemiesFound);
        
        Destroy(gameObject, 0.5f);
    }

    private void FindEnemies(int timesToChain, Transform fromEnemy, HashSet<Transform> enemiesHit, List<Transform> enemiesFound)
    {
        enemiesHit.Add(fromEnemy);
        enemiesFound.Add(fromEnemy);
        if (timesToChain == 0) 
            return ;
        Collider[] colliders = Physics.OverlapSphere(fromEnemy.position, 6f);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                if (!enemiesHit.Contains(collider.transform))
                {
                    if (CanAttack(collider.GetComponent<EndlessEnemy>()))
                    {
                        FindEnemies(timesToChain - 1, collider.transform, enemiesHit, enemiesFound);
                        return;
                    }
                }
            }
        }
    }

    private void ChainEnemies(List<Transform> enemies)
    {
        Transform chainFrom = transform;
        Transform chainTo = null;
        foreach (Transform enemy in enemies)
        {
            chainTo = enemy;
            GameObject chainObject = Instantiate(chainPrefab, chainFrom);
            LightningBoltScript lightningBoltScript = chainObject.GetComponent<LightningBoltScript>();
            lightningBoltScript.StartObject = chainFrom.gameObject;
            lightningBoltScript.EndObject = chainTo.gameObject;
            Damage(enemy);
            chainFrom = chainTo;
            Destroy(chainObject, 0.5f);
        }
    }

    protected override void InitializeProjectile()
    {
        HitTarget();
    }
}
