using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    [SerializeField]
    private GameObject particleEffect;
    [SerializeField]
    private Transform particleSpawnPoint;

    public void Awake()
    {
        if (particleEffect == null)
            Debug.LogError("Missing particle effect");
        if (particleSpawnPoint == null)
            Debug.LogError("Missing particle spawn point");
    }

    public void ShowParticleEffects()
    {
        GameObject particleEffectObject = (GameObject)Instantiate(particleEffect, particleSpawnPoint.position, particleSpawnPoint.rotation);
        ParticleSystem parts = particleEffectObject.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration;
        Destroy(particleEffectObject, totalDuration);
    }
}
