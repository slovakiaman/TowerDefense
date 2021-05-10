using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private Vector3 positionOffset = new Vector3(0f, 2f, 0f);

    private BuildingBlock selectedBlock;

    private bool disabled = false;

    private Shop shop;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        shop = UIManager.instance.GetShop();
    }

    public void SetShop(Shop shop)
    {
        this.shop = shop;
    }

    public BuildingBlock GetSelectedBlock()
    {
        return selectedBlock;
    }

    public void SetSelectedBlock(BuildingBlock block)
    {
        if (disabled)
            return;
        if (selectedBlock != null)
            selectedBlock.Unselect();
        selectedBlock = block;
        selectedBlock.Select();
        shop.SetSelectedTower(selectedBlock.GetTower());
    }

    public void BuildTower(GameObject tower)
    {
        if (selectedBlock.GetTower() != null)
            return;

        GameObject towerObject = (GameObject)Instantiate(tower, selectedBlock.transform.position + positionOffset, selectedBlock.transform.rotation);
        selectedBlock.SetTower(towerObject);
        shop.SetSelectedTower(towerObject);
    }

    public void DestroyTower(GameObject tower)
    {
        Destroy(tower);
    }

    public void UpgradeTower(GameObject tower)
    {
        GameObject upgradeTower = tower.GetComponent<Tower>().GetUpgradeTower();
        if (upgradeTower != null)
        {
            DestroyTower(selectedBlock.GetTower());
            selectedBlock.SetTower(null);
            BuildTower(tower.GetComponent<Tower>().GetUpgradeTower());
        }
    }

    public void Disable()
    {
        disabled = true;
        if (selectedBlock != null)
        {
            selectedBlock.Unselect();
            selectedBlock = null;
        }
    }

    public void ResetBuilder()
    {
        disabled = false;
        if (selectedBlock != null)
            selectedBlock.Unselect();
        selectedBlock = null;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject towerObject in gameObjects)
        {
            Destroy(towerObject);
        }
        shop.Reset();
    }
}
