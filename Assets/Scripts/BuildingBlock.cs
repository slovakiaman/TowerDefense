using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBlock : MonoBehaviour
{

    private Renderer rend;
    private GameObject tower;

    private Color defaultColor;
    public Color hoverColor;
    public Color selectedColor;

    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        //cant click on buildingblock if UI is over it
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (BuildManager.instance.getTowerToBuild() == null)
        {
            return;
        }*/
        if (!isSelected)
            rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (!isSelected)
            rend.material.color = defaultColor;
    }

    private void OnMouseDown()
    {
        BuildManager.instance.SetSelectedBlock(this);
    }

    public void SetTower(GameObject tower)
    {
        this.tower = tower;
    }

    public GameObject GetTower()
    {
        return tower;
    }

    public void Select()
    {
        isSelected = true;
        rend.material.color = selectedColor;
        if (tower != null)
            tower.GetComponent<Tower>().ShowRadiusCircle(true);
    }

    public void Unselect()
    {
        isSelected = false;
        rend.material.color = defaultColor;
        if (tower != null)
            tower.GetComponent<Tower>().ShowRadiusCircle(false);
    }

}
