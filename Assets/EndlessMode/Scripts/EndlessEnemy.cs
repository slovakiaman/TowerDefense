﻿using UnityEngine;
using UnityEngine.UI;

public class EndlessEnemy : MonoBehaviour
{
    public float speed = 10f;
    public float health;
    public float maxHealth = 100f;
    public int moneyReward = 50;
    public int scoreReward = 10;

    public EnemyVariant variant;
    public WeakAgainstType weakAgainstType;

    public int pathNumber;
    private Transform nextWaypoint;
    private int nextWaypointIndex = 0;
    
    private Image HPComponent;
    private Transform bodyTransform;

    private float actualSpeed;
    private float randomSpeedTimeRemaining;
    private float randomSpeedCooldown;
    private float onHitSpeedTimeRemaining;
    private float randomInvincibilityTimeRemaining;
    private float randomInvincibilityCooldown;

    public Animator controller;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private float playWithDelay;

    private bool dead = false;

    void Start()
    {
        health = maxHealth;
        nextWaypoint = Waypoints.instance.GetNextWaypoint(pathNumber, -1);
        HPComponent = transform.Find("Canvas").Find("HPBar").Find("HP").GetComponent<Image>();
        bodyTransform = transform.Find("EnemyModel").GetComponent<Transform>();
        if (speed == 0)
            Debug.LogError("Speed too low");
        if (maxHealth == 0)
            Debug.LogError("Max health too low");
        if (nextWaypoint == null)
            Debug.LogError("Next waypoint null");
        if (controller == null)
            Debug.LogError("No animation controller");
        if (deathSound == null)
            Debug.LogError("No death sound attached");
    }

    void Update()
    {
        if (!dead)
        {
            Vector3 direction = nextWaypoint.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, nextWaypoint.position) <= 0.2f)
            {
                this.nextWaypoint = Waypoints.instance.GetNextWaypoint(pathNumber, nextWaypointIndex);
                if (nextWaypoint == null)
                {
                    EnterExit();
                    return;
                }
                nextWaypointIndex++;
                bodyTransform.LookAt(nextWaypoint.position);
            }

            HandleUpdateEventChecks();
        }
    }

    public void SetupValues(int pathNumber, float speed, float maxHealth, int moneyReward, int scoreReward, float scale)
    {
        this.pathNumber = pathNumber;
        this.speed = speed;
        this.actualSpeed = speed;
        this.maxHealth = maxHealth;
        this.moneyReward = moneyReward;
        this.scoreReward = scoreReward;
        this.gameObject.transform.localScale = Vector3.one * scale;
    }

    public void TakeDamage(int damage)
    {
        if (EndlessValueCalculator.instance.IsEventActive(EndlessLevelEventID.MONSTER_RANDOM_INVINCIBILITY) && this.randomInvincibilityCooldown <= 0f)
        {
            this.randomInvincibilityCooldown = Random.Range(5, 11);
            this.randomInvincibilityTimeRemaining = 3f;
        }
        else
        {
            health -= damage;
            float hpPercent = ((float)health) / ((float)maxHealth);
            HPComponent.fillAmount = hpPercent;
            if (health <= 0)
                Die();
        }
        if (EndlessValueCalculator.instance.IsEventActive(EndlessLevelEventID.MONSTER_ONHIT_SPEED))
        {
            this.onHitSpeedTimeRemaining = 3f;
            this.actualSpeed = this.speed * 1.2f;
        } 
        else if (EndlessValueCalculator.instance.IsEventActive(EndlessLevelEventID.MONSTER_ONHIT_SLOW))
        {
            this.onHitSpeedTimeRemaining = 3f;
            this.actualSpeed = this.speed * 0.8f;
        }
    }

    private void Die()
    {
        dead = true;
        controller.SetBool("dead", true);
        gameObject.tag = "Dead";
        transform.Find("Canvas").gameObject.SetActive(false);
        EndlessPlayerManager.instance.AddMoney(moneyReward);
        ScoreManager.instance.AddScore(scoreReward);
        if (playWithDelay > 0)
        {
            deathSound.PlayDelayed(playWithDelay);
        } else
        {
            deathSound.Play();
        }
    }

    private void HandleUpdateEventChecks()
    {
        if (this.randomSpeedTimeRemaining > -1)
            this.randomSpeedTimeRemaining -= Time.deltaTime;
        if (this.onHitSpeedTimeRemaining > -1)
            this.onHitSpeedTimeRemaining -= Time.deltaTime;
        if (this.randomInvincibilityTimeRemaining > -1)
            this.randomInvincibilityTimeRemaining -= Time.deltaTime;
            
        if (this.randomSpeedTimeRemaining <= 0 && this.onHitSpeedTimeRemaining <= 0)
            this.actualSpeed = this.speed;
            
        if (this.randomSpeedTimeRemaining <= 0)
            this.randomSpeedCooldown -= Time.deltaTime;
        if (this.randomInvincibilityTimeRemaining <= 0)
            this.randomInvincibilityCooldown -= Time.deltaTime;
        
        if (EndlessValueCalculator.instance.IsEventActive(EndlessLevelEventID.MONSTER_BUFF_SPEED_RANDOM))
        {
            if (this.randomSpeedCooldown <= 0f)
            {
                this.randomSpeedTimeRemaining = 3f;
                this.randomSpeedCooldown = Random.Range(3, 11);
                this.actualSpeed = this.speed * 1.5f;
            }
        } 
        else if (EndlessValueCalculator.instance.IsEventActive(EndlessLevelEventID.MONSTER_DEBUFF_SPEED_RANDOM))
        {
            if (this.randomSpeedCooldown <= 0f)
            {
                this.randomSpeedTimeRemaining = 3f;
                this.randomSpeedCooldown = Random.Range(3, 11);
                this.actualSpeed = this.speed * 0.5f;
            }
        }
    }

    private void EnterExit()
    {
        EndlessPlayerManager.instance.DecreaseLives(1);
        ScoreManager.instance.setScore((ulong)(ScoreManager.instance.GetScore() * 0.9));
        Destroy(gameObject);
    }
}