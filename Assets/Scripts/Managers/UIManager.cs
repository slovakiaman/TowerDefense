using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("SETUP FIELDS")]
    private WaveManager WaveManager;
    private GameManager GameManager;

    [Header("Next wave panel")]
    [SerializeField] public Text nextWaveCountdownText;
    [SerializeField] public GameObject startButton;
    [SerializeField] public GameObject spawnWaveButton;

    [Header("Shop panel")]
    [SerializeField] public Text moneyText;
    [SerializeField] public Text livesText;
    [SerializeField] private GameObject shopLeftPanel;
    [SerializeField] private GameObject shopRightPanel;
    [SerializeField] private GameObject shopBuyButton;
    [SerializeField] private GameObject shopSellButton;
    [SerializeField] private GameObject shopUpgradeButton;
    [SerializeField] private GameObject nextEnemyImage;
    [SerializeField] private GameObject WeakAgainstImage;
    [SerializeField] private Text WeakAgainstText;

    [Header("Towers panel")]
    [SerializeField] private GameObject balistaItem;
    [SerializeField] private GameObject cannonItem;
    [SerializeField] private GameObject crystalItem;
    [SerializeField] private GameObject teslaItem;
    [SerializeField] private GameObject spinnerItem;

    [Header("Menu Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    [Header("DialoguePanels")]
    [SerializeField] private GameObject infoDialoguePanel;
    [SerializeField] private GameObject kingDialoguePanel;
    [SerializeField] private GameObject otherDialoguePanel;

    [Header("NextWaveEnemy")]
    [SerializeField] private GameObject nextWaveSlimeRed;
    [SerializeField] private GameObject nextWaveSlimeGreen;
    [SerializeField] private GameObject nextWaveSlimeYellow;
    [SerializeField] private GameObject nextWaveTurtleBlue;
    [SerializeField] private GameObject nextWaveTurtleBlack;
    [SerializeField] private GameObject nextWaveTurtleWhite;
    [SerializeField] private GameObject nextWaveMummyGrey;
    [SerializeField] private GameObject nextWaveMummyWhite;
    [SerializeField] private GameObject nextWaveMummyRed;
    [SerializeField] private GameObject nextWaveGolemGreen;
    [SerializeField] private GameObject nextWaveGolemRed;
    [SerializeField] private GameObject nextWaveGolemRed2;
    [SerializeField] private GameObject nextWaveLichPurple;
    [SerializeField] private GameObject nextWaveLichGreen;
    [SerializeField] private GameObject nextWaveLichRed;
    [SerializeField] private GameObject nextWaveBeholderPurple;
    [SerializeField] private GameObject nextWaveBeholderCyan;
    [SerializeField] private GameObject nextWaveBeholderGreen;

    private TextAnimator textAnimator;

    private void Awake()
    {
        instance = this;
        shopLeftPanel.SetActive(false);
        shopRightPanel.SetActive(false);
        shopBuyButton.SetActive(false);
        shopSellButton.SetActive(false);
        shopUpgradeButton.SetActive(false);

        GameObject GameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        this.GameManager = (GameManager)GameManagerObject.GetComponent<GameManager>();
        this.WaveManager = (WaveManager)GameManagerObject.GetComponent<WaveManager>();

        this.textAnimator = gameObject.GetComponent<TextAnimator>();

    }

    public void InitShopTowers()
    {
        ConstantsManager instance = ConstantsManager.instance;
        if (ConstantsManager.instance.balistaTowerPrefab1 == null)
            balistaItem.SetActive(false);

        if (ConstantsManager.instance.cannonTowerPrefab1 == null)
            cannonItem.SetActive(false);

        if (ConstantsManager.instance.crystalTowerPrefab1 == null)
            crystalItem.SetActive(false);

        if (ConstantsManager.instance.teslaTowerPrefab1 == null)
            teslaItem.SetActive(false);

        if (ConstantsManager.instance.spinnerTowerPrefab1 == null)
            spinnerItem.SetActive(false);
    }

    public Shop GetShop()
    {
        return shopLeftPanel.GetComponentInParent<Shop>();
    }

    public void ShowMoney(int ammount)
    {
        moneyText.text = "$" + ammount;
    }

    public void ShowLives(int ammount)
    {
        livesText.text = ammount + " lives";
    }

    public void ShowGameNotStartedText()
    {
        nextWaveCountdownText.text = "Press button to start";
    }

    public void ShowWaveSpawning(int waveNumber)
    {
        nextWaveCountdownText.text = "Spawning wave " + waveNumber;
    }

    public void ShowNextWaveCountdown(double countdown)
    {
        nextWaveCountdownText.text = "Next wave in " + countdown + " seconds";
    }

    public void ShowTowerBaseInfo(Tower tower)
    {
        shopRightPanel.SetActive(true);
        Text towerName = shopRightPanel.transform.Find("TowerNameText").GetComponent<Text>();
        Text towerStats = shopRightPanel.transform.Find("TowerStatsText").GetComponent<Text>();

        string pom = "Range :" + tower.GetTowerRange();
        pom += "\n" + "Speed :" + tower.GetTowerFireRate();
        pom += "\n" + "Damage :" + tower.GetTowerDamage();

        towerName.text = tower.GetTowerName();
        towerStats.text = pom;

        List<WeakAgainstType> weakAgainst = tower.GetWeakAgainst();
        string weak = "";
        foreach (WeakAgainstType enemyType in weakAgainst)
        {
            weak += enemyType.ToString() + "\n";
        }
        this.WeakAgainstText.text = weak;
    }

    public void ShowTowerBaseUpgradeInfo(Tower tower)
    {
        shopRightPanel.SetActive(true);
        Tower upgradeTower = tower.GetUpgradeTower().GetComponent<Tower>();

        Text towerName = shopRightPanel.transform.Find("TowerNameText").GetComponent<Text>();
        Text towerStats = shopRightPanel.transform.Find("TowerStatsText").GetComponent<Text>();

        string pom = ">> " + upgradeTower.GetTowerName();
        pom += "\n" + "Upgrade price :" + upgradeTower.GetTowerPrice();
        pom += "\n" + "Range :" + tower.GetTowerRange() + " >> " + upgradeTower.GetTowerRange();
        pom += "\n" + "Speed :" + tower.GetTowerFireRate() + " >> " + upgradeTower.GetTowerFireRate();
        pom += "\n" + "Damage :" + tower.GetTowerDamage() + " >> " + upgradeTower.GetTowerDamage();

        towerName.text = tower.GetTowerName();
        towerStats.text = pom;

        List<WeakAgainstType> weakAgainst = tower.GetWeakAgainst();
        string weak = "";
        foreach (WeakAgainstType enemyType in weakAgainst)
        {
            weak += enemyType.ToString() + "\n";
        }
        this.WeakAgainstText.text = weak;
    }

    public void ShopSelectedBlock()
    {
        ShowShopLeftPanel(true);
        ShowShopRightPanel(false);
    }

    public void ShopSelectedTower(Tower tower)
    {
        ShowTowerBaseInfo(tower);
        if (PlayerManager.instance.CanBuyTower(tower))
        {
            ShowBuyButton(true);
            this.shopBuyButton.transform.Find("Text").GetComponent<Text>().text = "BUY" + " " + "($" + tower.GetTowerPrice() + ")";
        }
        else
        {
            ShowBuyButton(false);
        }
        ShowSellButton(false);
        ShowUpgradeButton(false);
    }

    public void ShopUpgradeableTower(Tower tower)
    {
        ShowTowerBaseUpgradeInfo(tower);
        Tower upgradeTowerCompoment = tower.GetUpgradeTower().GetComponent<Tower>();
        if (PlayerManager.instance.CanBuyTower(upgradeTowerCompoment))
        {
            ShowUpgradeButton(true);
            this.shopUpgradeButton.transform.Find("Text").GetComponent<Text>().text = "UPGRADE" + " " + "($" + upgradeTowerCompoment.GetTowerPrice() + ")";
        }
        else
        {
            ShowUpgradeButton(false);
        }
        ShowSellButton(true);
        this.shopSellButton.transform.Find("Text").GetComponent<Text>().text = "SELL" + " " + "($" + tower.GetTowerPrice() / 2 + ")";
        ShowBuyButton(false);
    }

    public void ShopFinalTower(Tower tower)
    {
        ShowTowerBaseInfo(tower);
        ShowBuyButton(false);
        ShowSellButton(true);
        this.shopSellButton.transform.Find("Text").GetComponent<Text>().text = "SELL" + " " + "($" + tower.GetTowerPrice() / 2 + ")";
        ShowUpgradeButton(false);
    }

    public void ShowShopLeftPanel(bool show)
    {
        shopLeftPanel.SetActive(show);
    }
    public void ShowShopRightPanel(bool show)
    {
        this.shopRightPanel.SetActive(show);
    }

    public void ShowBuyButton(bool show)
    {
        this.shopBuyButton.SetActive(show);
    }

    public void ShowSellButton(bool show)
    {
        this.shopSellButton.SetActive(show);
    }

    public void ShowUpgradeButton(bool show)
    {
        this.shopUpgradeButton.SetActive(show);
    }

    public void ShowStartButton(bool show)
    {
        this.startButton.SetActive(show);
    }

    public void ShowSpawnWaveButton(bool show)
    {
        this.spawnWaveButton.SetActive(show);
    }

    public void ShowGameOver(bool show)
    {
        defeatPanel.SetActive(show);
    }

    public void ShowVictory(bool show)
    {
        victoryPanel.SetActive(show);
    }

    public void ShowPause(bool show)
    {
        pausePanel.SetActive(show);
    }

    public void ShowInfoDialoguePanel(bool show)
    {
        this.infoDialoguePanel.SetActive(show);
    }

    public void ShowKingDialoguePanel(bool show)
    {
        this.kingDialoguePanel.SetActive(show);
    }

    public void ShowOtherDialoguePanel(bool show)
    {
        this.otherDialoguePanel.SetActive(show);
    }

    public void StartSpawner()
    {
        ShowStartButton(false);
        ShowSpawnWaveButton(true);
        WaveManager.StartSpawner();
        GameManager.PlayBackgroundMusic();
    }

    public void SpawnAnotherWave()
    {
        WaveManager.spawnAnotherWave();
    }

    public void NextLevel()
    {
        GameManager.NextLevel();
    }

    public void ChangeLevel()
    {
        GameManager.ChangeLevel();
    }

    public void ToggleSounds()
    {
        GameManager.ToggleSounds();
    }

    public void Pause()
    {
        GameManager.Pause();
    }

    public void Quit()
    {
        GameManager.Quit();
    }

    public void Restart()
    {
        GameManager.ResetGame();
        shopLeftPanel.SetActive(false);
        shopRightPanel.SetActive(false);
        shopBuyButton.SetActive(false);
        shopSellButton.SetActive(false);
        shopUpgradeButton.SetActive(false);
        pausePanel.SetActive(false);
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    public void Menu()
    {
        GameManager.Menu();
    }

    public void ShowDialogueLine(DialogueLine dialogueLine)
    {
        DialogueEntity entity = dialogueLine.GetDialogueEntity();
        if (entity == DialogueEntity.KING)
        {
            ShowKingDialoguePanel(true);
            Text textObject = kingDialoguePanel.transform.Find("DialogText").GetComponent<Text>();
            kingDialoguePanel.transform.Find("NameHeader").GetComponent<Text>().text = "Lightsoul II";
            textAnimator.AnimateDialogueLine(textObject, dialogueLine.GetText());
            Invoke("HideKingDialoguePanel", dialogueLine.GetTimeToDisappear());
        } 
        else if(entity == DialogueEntity.INFO)
        {
            ShowInfoDialoguePanel(true);
            Text textObject = infoDialoguePanel.transform.Find("DialogText").GetComponent<Text>();
            infoDialoguePanel.transform.Find("NameHeader").GetComponent<Text>().text = "Meanwhile...";
            textAnimator.AnimateDialogueLine(textObject, dialogueLine.GetText());
            Invoke("HideInfoDialoguePanel", dialogueLine.GetTimeToDisappear());
        }
        else
        {
            ShowOtherDialoguePanel(true);
            Text textObject = otherDialoguePanel.transform.Find("DialogText").GetComponent<Text>();
            String name = "";
            switch (entity)
            {
                case DialogueEntity.QUEEN:
                    name = "Queen";
                    break;
                case DialogueEntity.GENERAL1:
                    name = "General";
                    break;
                case DialogueEntity.VILLAGER:
                    name = "Villager";
                    break;
                case DialogueEntity.GENERAL2:
                    name = "General2";
                    break;
                case DialogueEntity.GENERAL3:
                    name = "General3";
                    break;
                case DialogueEntity.BOSS1:
                    name = "First boss";
                    break;
                case DialogueEntity.BOSS2:
                    name = "Second boss";
                    break;
                case DialogueEntity.BOSS3:
                    name = "Third boss";
                    break;
                case DialogueEntity.WIZARD:
                    name = "Wizard Rewind";
                    break;
            }
            otherDialoguePanel.transform.Find("NameHeader").GetComponent<Text>().text = name;
            GameObject entityImageObject = otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).gameObject;
            entityImageObject.SetActive(true);
            textAnimator.AnimateDialogueLine(textObject, dialogueLine.GetText());
            Invoke("HideOtherDialoguePanel", dialogueLine.GetTimeToDisappear());
        }
    }

    public void HideInfoDialoguePanel()
    {
        ShowInfoDialoguePanel(false);
    }

    public void HideKingDialoguePanel()
    {
        ShowKingDialoguePanel(false);
    }

    public void HideOtherDialoguePanel()
    {
        ShowOtherDialoguePanel(false);
        foreach (string entity in Enum.GetNames(typeof(DialogueEntity)))
        {
            if (!(entity == Enum.GetName(typeof(DialogueEntity), DialogueEntity.KING) || entity == Enum.GetName(typeof(DialogueEntity), DialogueEntity.INFO)))
                this.otherDialoguePanel.transform.Find(entity).gameObject.SetActive(false);
        }
    }

    public void ShowNextWaveEnemy(EnemyVariant variant, bool show)
    {
        switch (variant)
        {
            case EnemyVariant.SLIME_RED:
                nextWaveSlimeRed.SetActive(show);
                break;
            case EnemyVariant.SLIME_GREEN:
                nextWaveSlimeGreen.SetActive(show);
                break;
            case EnemyVariant.SLIME_YELLOW:
                nextWaveSlimeYellow.SetActive(show);
                break;
            case EnemyVariant.TURTLE_BLUE:
                nextWaveTurtleBlue.SetActive(show);
                break;
            case EnemyVariant.TURTLE_BLACK:
                nextWaveTurtleBlack.SetActive(show);
                break;
            case EnemyVariant.TURTLE_WHITE:
                nextWaveTurtleWhite.SetActive(show);
                break;
            case EnemyVariant.MUMMY_GREY:
                nextWaveMummyGrey.SetActive(show);
                break;
            case EnemyVariant.MUMMY_WHITE:
                nextWaveMummyWhite.SetActive(show);
                break;
            case EnemyVariant.MUMMY_RED:
                nextWaveMummyRed.SetActive(show);
                break;
            case EnemyVariant.GOLEM_GREEN:
                nextWaveGolemGreen.SetActive(show);
                break;
            case EnemyVariant.GOLEM_RED:
                nextWaveGolemRed.SetActive(show);
                break;
            case EnemyVariant.GOLEM_RED2:
                nextWaveGolemRed2.SetActive(show);
                break;
            case EnemyVariant.LICH_PURPLE:
                nextWaveLichPurple.SetActive(show);
                break;
            case EnemyVariant.LICH_GREEN:
                nextWaveLichGreen.SetActive(show);
                break;
            case EnemyVariant.LICH_RED:
                nextWaveLichRed.SetActive(show);
                break;
            case EnemyVariant.BEHOLDER_PURPLE:
                nextWaveBeholderPurple.SetActive(show);
                break;
            case EnemyVariant.BEHOLDER_CYAN:
                nextWaveBeholderCyan.SetActive(show);
                break;
            case EnemyVariant.BEHOLDER_GREEN:
                nextWaveBeholderGreen.SetActive(show);
                break;
        }
    }

}
