﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSingleTargetProjectile : EndlessProjectile
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
