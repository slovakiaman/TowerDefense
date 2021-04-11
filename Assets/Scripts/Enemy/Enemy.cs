using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
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
        }
    }

    public void SetupValues(int pathNumber, float speed, float maxHealth, int moneyReward, float scale)
    {
        this.pathNumber = pathNumber;
        this.speed = speed;
        this.maxHealth = maxHealth;
        this.moneyReward = moneyReward;
        this.gameObject.transform.localScale = Vector3.one * scale;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        float hpPercent = ((float)health) / ((float)maxHealth);
        HPComponent.fillAmount = hpPercent;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        dead = true;
        controller.SetBool("dead", true);
        gameObject.tag = "Dead";
        transform.Find("Canvas").gameObject.SetActive(false);
        PlayerManager.instance.AddMoney(moneyReward);
        ScoreManager.instance.AddScore(scoreReward);
        if (playWithDelay > 0)
        {
            deathSound.PlayDelayed(playWithDelay);
        } else
        {
            deathSound.Play();
        }
    }

    private void EnterExit()
    {
        PlayerManager.instance.DecreaseLives(1);
        ScoreManager.instance.setScore((ulong)(ScoreManager.instance.GetScore() * 0.9));
        Destroy(gameObject);
    }

}
