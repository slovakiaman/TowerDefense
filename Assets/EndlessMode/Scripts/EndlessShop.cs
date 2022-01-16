using UnityEngine;

public class EndlessShop : MonoBehaviour
{
    private EndlessBuildManager buildManager;

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
            if (selectedTower.GetComponent<EndlessTower>().GetUpgradeTower() != null)
                shopState = MenuState.TOWER_UPGRADEABLE;
            else 
                shopState = MenuState.TOWER_FINAL;
    }

    public void Buy()
    {
        shopState = MenuState.TOWER_UPGRADEABLE;
        buildManager.BuildTower(selectedTower);
        EndlessPlayerManager.instance.AddMoney(-1 * selectedTower.GetComponent<EndlessTower>().GetTowerPrice());
        moneySound.Play();
    }

    public void Sell()
    {
        shopState = MenuState.SELECTED_BLOCK;
        buildManager.DestroyTower(selectedTower);
        EndlessPlayerManager.instance.AddMoney((int) (0.5 * selectedTower.GetComponent<EndlessTower>().GetTowerPrice()));
        EndlessUIManager.instance.ShowShopRightPanel(false);
        moneySound.Play();
    }
    public void Upgrade()
    {
        GameObject upgradeTower = selectedTower.GetComponent<EndlessTower>().GetUpgradeTower();
        if (upgradeTower != null)
            shopState = MenuState.TOWER_UPGRADEABLE;
        else
            shopState = MenuState.TOWER_FINAL;

        buildManager.UpgradeTower(selectedTower);
        EndlessPlayerManager.instance.AddMoney(-1 * selectedTower.GetComponent<EndlessTower>().GetTowerPrice());
        moneySound.Play();
    }
    void Start()
    {
        buildManager = EndlessBuildManager.instance;    
    }

    void Update()
    {
        switch (shopState)
        {
            case MenuState.NOTHING_SELECTED:
                break;
            case MenuState.SELECTED_BLOCK:
                EndlessUIManager.instance.ShopSelectedBlock();
                break;
            case MenuState.SELECTED_TOWER:
                EndlessUIManager.instance.ShopSelectedTower(selectedTower.GetComponent<EndlessTower>());
                break;
            case MenuState.TOWER_UPGRADEABLE:
                EndlessUIManager.instance.ShopUpgradeableTower(selectedTower.GetComponent<EndlessTower>());
                break;
            case MenuState.TOWER_FINAL:
                EndlessUIManager.instance.ShopFinalTower(selectedTower.GetComponent<EndlessTower>());
                break;
        }
    }

    public void Reset()
    {
        this.selectedTower = null;
        this.shopState = MenuState.NOTHING_SELECTED;
    }
}