using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessUIManager : UIManager
{
    protected void Awake()
    {
        instance = this;
        shopLeftPanel.SetActive(false);
        shopRightPanel.SetActive(false);
        shopBuyButton.SetActive(false);
        shopSellButton.SetActive(false);
        shopUpgradeButton.SetActive(false);
        GameObject GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager = (EndlessGameManager)GameManagerObject.GetComponent<EndlessGameManager>();
        WaveManager = (EndlessWaveManager)GameManagerObject.GetComponent<EndlessWaveManager>();
    }

    protected void Start()
    {
        
    }

}

