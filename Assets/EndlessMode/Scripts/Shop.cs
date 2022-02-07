using EndlessMode.Towers;

namespace EndlessMode.Shop
{
    using EndlessMode.Managers;
    using UnityEngine;

    public class Shop : MonoBehaviour
    {
        private BuildManager buildManager;

        enum MenuState {NOTHING_SELECTED, SELECTED_BLOCK, SELECTED_TOWER, TOWER_UPGRADEABLE, TOWER_FINAL };

        [SerializeField] private AudioSource moneySound;

        private MenuState shopState;
        private GameObject selectedTower;

        private void Awake()
        {
            shopState = MenuState.NOTHING_SELECTED;
        }

        public void SelectCannonTower()
        {
            if (buildManager.GetSelectedBlock().GetTower() == null)
            {
                selectedTower = ConstantsManager.instance.cannonTowerPrefab1;
                shopState = MenuState.SELECTED_TOWER;
            }
        }

        public void SelectBalistaTower()
        {
            if (buildManager.GetSelectedBlock().GetTower() == null)
            {
                selectedTower = ConstantsManager.instance.balistaTowerPrefab1;
                shopState = MenuState.SELECTED_TOWER;
            }
        }

        public void SelectCrystalTower()
        {
            if (buildManager.GetSelectedBlock().GetTower() == null)
            {
                selectedTower = ConstantsManager.instance.crystalTowerPrefab1;
                shopState = MenuState.SELECTED_TOWER;
            }
        }

        public void SelectTeslaTower()
        {
            if (buildManager.GetSelectedBlock().GetTower() == null)
            {
                selectedTower = ConstantsManager.instance.teslaTowerPrefab1;
                shopState = MenuState.SELECTED_TOWER;
            }
        }

        public void SelectSpinnerTower()
        {
            if (buildManager.GetSelectedBlock().GetTower() == null)
            {
                selectedTower = ConstantsManager.instance.spinnerTowerPrefab1;
                shopState = MenuState.SELECTED_TOWER;
            }
        }

        public void SetSelectedTower(GameObject tower)
        {
            selectedTower = tower;
            if (selectedTower == null)
                shopState = MenuState.SELECTED_BLOCK;
            else
                if (selectedTower.GetComponent<Tower>().GetUpgradeTower() != null)
                    shopState = MenuState.TOWER_UPGRADEABLE;
                else 
                    shopState = MenuState.TOWER_FINAL;
        }

        public void Buy()
        {
            shopState = MenuState.TOWER_UPGRADEABLE;
            buildManager.BuildTower(selectedTower);
            int towerCost = ValueCalculator.instance.CalculateTowerCOST(selectedTower.GetComponent<Tower>());
            PlayerManager.instance.AddMoney(-1 * towerCost);
            moneySound.Play();
        }

        public void Sell()
        {
            shopState = MenuState.SELECTED_BLOCK;
            buildManager.DestroyTower(selectedTower);
            PlayerManager.instance.AddMoney((int) (0.5 * selectedTower.GetComponent<Tower>().GetTowerPrice()));
            UIManager.instance.ShowShopRightPanel(false);
            moneySound.Play();
        }
        public void Upgrade()
        {
            GameObject upgradeTower = selectedTower.GetComponent<Tower>().GetUpgradeTower();
            if (upgradeTower != null)
                shopState = MenuState.TOWER_UPGRADEABLE;
            else
                shopState = MenuState.TOWER_FINAL;

            buildManager.UpgradeTower(selectedTower);
            PlayerManager.instance.AddMoney(-1 * selectedTower.GetComponent<Tower>().GetTowerPrice());
            moneySound.Play();
        }
        void Start()
        {
            buildManager = BuildManager.instance;    
        }

        void Update()
        {
            switch (shopState)
            {
                case MenuState.NOTHING_SELECTED:
                    break;
                case MenuState.SELECTED_BLOCK:
                    UIManager.instance.ShopSelectedBlock();
                    break;
                case MenuState.SELECTED_TOWER:
                    UIManager.instance.ShopSelectedTower(selectedTower.GetComponent<Tower>());
                    break;
                case MenuState.TOWER_UPGRADEABLE:
                    UIManager.instance.ShopUpgradeableTower(selectedTower.GetComponent<Tower>());
                    break;
                case MenuState.TOWER_FINAL:
                    UIManager.instance.ShopFinalTower(selectedTower.GetComponent<Tower>());
                    break;
            }
        }

        public void Reset()
        {
            this.selectedTower = null;
            this.shopState = MenuState.NOTHING_SELECTED;
        }
    }   
}