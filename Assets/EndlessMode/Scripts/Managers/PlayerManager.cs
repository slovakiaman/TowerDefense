using EndlessMode.Managers;
using EndlessMode.Towers;
using UnityEngine;

namespace EndlessMode.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
    
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
            UIManager.instance.ShowMoney(money);
            UIManager.instance.ShowLives(lives);
        }
    
        public void AddMoney(int ammount)
        {
            this.money += ammount;
            UIManager.instance.ShowMoney(this.money);
        }
    
        public int GetMoney()
        {
            return this.money;
        }
        public void DecreaseLives(int ammount)
        {
            this.lives -= ammount;
            UIManager.instance.ShowLives(this.lives);
        }
    
        public int GetLives()
        {
            return this.lives;
        }
    
        public bool CanBuyTower(Tower tower)
        {
            return this.money >= tower.GetTowerPrice();
        }
    
        public void ResetPlayer()
        {
            this.money = startMoney;
            this.lives = startLives;
            this.victory = false;
            UIManager.instance.ShowMoney(money);
            UIManager.instance.ShowLives(lives);
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
}
