using EndlessMode.Enemies;
using EndlessMode.Managers;

namespace EndlessMode.Towers
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Tower : MonoBehaviour
    {
        public TowerType towerType;
        [SerializeField] private string towerName;
        [SerializeField] private GameObject radiusCircle;

        [Header("Tower stats")]
        [SerializeField] private int price;
        [SerializeField] private int scorePrice;
        [SerializeField] protected float range = 10f;
        [SerializeField] protected float fireRate = 1f;
        [SerializeField] protected int damage = 10;
        [SerializeField] protected List<WeakAgainstType> weakAgainst = new List<WeakAgainstType>();

        [Header("UpgradedTower")]
        [SerializeField] private GameObject upgradeTower;

        [Header("Setup fields")]
        protected string enemyTag = "Enemy";
        [SerializeField] protected AudioSource shootSound;

        [Header("Animated")]
        [SerializeField] protected Animator controller;
        protected ParticleObject particles;
        
        protected float reloadTimer = 0f;
        protected float inactiveTreshold = 0.2f;

        void Start()
        {
            ScoreManager.instance.AddScore(-scorePrice);
            InitializeTower();
            if (fireRate == 0)
                Debug.LogError("Fire rate too low");
            if (range == 0)
                Debug.LogError("Range too low");
            if (radiusCircle == null)
                Debug.LogError("No radius circle assigned");
            if (shootSound == null)
                Debug.LogError("No shoot sound attached");
            particles = transform.GetComponent<ParticleObject>();
    }

        void Update()
        {
            UpdateTower();
            Attack();
            ReloadUpdate();
        }

        public string GetTowerName()
        {
            return this.towerName;
        }

        public int GetTowerPrice()
        {
            return this.price;
        }

        public float GetTowerRange()
        {
            return this.range;
        }

        public float GetTowerFireRate()
        {
            return this.fireRate;
        }

        public int GetTowerDamage()
        {
            return this.damage;
        }

        public GameObject GetUpgradeTower()
        {
            return this.upgradeTower;
        }

        public List<WeakAgainstType> GetWeakAgainst()
        {
            return this.weakAgainst;
        }

        protected abstract void InitializeTower();
        protected abstract void UpdateTower();
        protected abstract void Attack();
        protected abstract void ReloadUpdate();

        protected void ShowParticles()
        {
            if (particles != null)
                particles.ShowParticleEffects();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public void ShowRadiusCircle(bool show)
        {
            this.radiusCircle.SetActive(show);
        }

        public bool CanAttack(Enemy enemy)
        {
            return !weakAgainst.Contains(enemy.weakAgainstType);
        }

    }
}