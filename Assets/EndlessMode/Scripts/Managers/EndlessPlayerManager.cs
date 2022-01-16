using UnityEngine;

public class EndlessPlayerManager : MonoBehaviour
{
    public static EndlessPlayerManager instance;

    private int money;
    private int lives;
    [SerializeField] private int startMoney;
    [SerializeField] private int startLives;

    private bool victory = false;

    private void Awake()
    {
        instance = this;
        if (startMoney == 0)
            Debug.LogError("start money too low");
        if (startLives == 0)
            Debug.LogError("start lives too low");
    }

    void Start()
    {
        money = startMoney;
        lives = startLives;
    }

    public void AddMoney(int ammount)
    {
        this.money += ammount;
        EndlessUIManager.instance.ShowMoney(this.money);
    }

    public int GetMoney()
    {
        return this.money;
    }
    public void DecreaseLives(int ammount)
    {
        this.lives -= ammount;
        EndlessUIManager.instance.ShowLives(this.lives);
    }

    public int GetLives()
    {
        return this.lives;
    }

    public bool CanBuyTower(EndlessTower tower)
    {
        return this.money >= tower.GetTowerPrice();
    }

    public void ResetPlayer()
    {
        this.money = startMoney;
        this.lives = startLives;
        this.victory = false;
        EndlessUIManager.instance.ShowMoney(money);
        EndlessUIManager.instance.ShowLives(lives);
    }

    public void SetVictory(bool value)
    {
        this.victory = value;
    }

    public bool GetVictory()
    {
        return this.victory;
    }


}