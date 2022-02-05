using NormalMode.Towers;

namespace NormalMode.Managers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NormalMode.Shop;
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [Header("SETUP FIELDS")]
        protected WaveManager waveManager;
        protected GameManager gameManager;

        [Header("Next wave panel")]
        public Text nextWaveCountdownText;
        public GameObject startButton;
        public GameObject spawnWaveButton;

        [Header("Menu Panels")]
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject defeatPanel;

        [Header("Shop panel")]
        [SerializeField] public Text moneyText;
        [SerializeField] public Text livesText;
        [SerializeField] public GameObject shopLeftPanel;
        [SerializeField] public GameObject shopRightPanel;
        [SerializeField] public GameObject shopBuyButton;
        [SerializeField] public GameObject shopSellButton;
        [SerializeField] public GameObject shopUpgradeButton;
        [SerializeField] public GameObject nextEnemyImage;
        [SerializeField] public GameObject WeakAgainstImage;
        [SerializeField] public Text WeakAgainstText;

        [Header("Towers panel")]
        [SerializeField] protected GameObject balistaItem;
        [SerializeField] protected GameObject cannonItem;
        [SerializeField] protected GameObject crystalItem;
        [SerializeField] protected GameObject teslaItem;
        [SerializeField] protected GameObject spinnerItem;

        [Header("NextWaveEnemy")]
        [SerializeField] protected GameObject nextWaveSlimeRed;
        [SerializeField] protected GameObject nextWaveSlimeGreen;
        [SerializeField] protected GameObject nextWaveSlimeYellow;
        [SerializeField] protected GameObject nextWaveTurtleBlue;
        [SerializeField] protected GameObject nextWaveTurtleBlack;
        [SerializeField] protected GameObject nextWaveTurtleWhite;
        [SerializeField] protected GameObject nextWaveMummyGrey;
        [SerializeField] protected GameObject nextWaveMummyWhite;
        [SerializeField] protected GameObject nextWaveMummyRed;
        [SerializeField] protected GameObject nextWaveGolemGreen;
        [SerializeField] protected GameObject nextWaveGolemRed;
        [SerializeField] protected GameObject nextWaveGolemRed2;
        [SerializeField] protected GameObject nextWaveLichPurple;
        [SerializeField] protected GameObject nextWaveLichGreen;
        [SerializeField] protected GameObject nextWaveLichRed;
        [SerializeField] protected GameObject nextWaveBeholderPurple;
        [SerializeField] protected GameObject nextWaveBeholderCyan;
        [SerializeField] protected GameObject nextWaveBeholderGreen;
        [SerializeField] protected GameObject nextWaveBoss1SoulEater;
        [SerializeField] protected GameObject nextWaveBoss2Nightmare;
        [SerializeField] protected GameObject nextWaveBoss3Usurper;
        [SerializeField] protected GameObject nextWaveBoss4TerrorBringer;
        [SerializeField] protected GameObject nextWaveBatPurple;
        [SerializeField] protected GameObject nextWaveBatWinter;
        [SerializeField] protected GameObject nextWaveBatDesert;
        [SerializeField] protected GameObject nextWaveSmallDragonRed;
        [SerializeField] protected GameObject nextWaveSmallDragonWinter;
        [SerializeField] protected GameObject nextWaveSmallDragonDesert;


        [Header("DialoguePanels")]
        [SerializeField] private GameObject infoDialoguePanel;
        [SerializeField] private GameObject kingDialoguePanel;
        [SerializeField] private GameObject otherDialoguePanel;
        
        private void Start()
        {
            InitShopTowers();
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

        public void StartSpawner()
        {
            ShowStartButton(false);
            ShowSpawnWaveButton(true);
            waveManager.StartSpawner();
            gameManager.PlayBackgroundMusic();
        }

        public void SpawnAnotherWave()
        {
            waveManager.spawnAnotherWave();
        }

        public void NextLevel()
        {
            gameManager.NextLevel();
        }

        public void ChangeLevel()
        {
            gameManager.ChangeLevel();
        }

        public void ToggleSounds()
        {
            gameManager.ToggleSounds();
        }

        public void Pause()
        {
            gameManager.Pause();
        }

        public void Quit()
        {
            gameManager.Quit();
        }

        public void Restart()
        {
            gameManager.ResetGame();
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
            gameManager.Menu();
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
                case EnemyVariant.BOSS1:
                    nextWaveBoss1SoulEater.SetActive(show);
                    break;
                case EnemyVariant.BOSS2:
                    nextWaveBoss2Nightmare.SetActive(show);
                    break;
                case EnemyVariant.BOSS3:
                    nextWaveBoss3Usurper.SetActive(show);
                    break;
                case EnemyVariant.BOSS4:
                    nextWaveBoss4TerrorBringer.SetActive(show);
                    break;
                case EnemyVariant.BAT_PURPLE:
                    nextWaveBatPurple.SetActive(show);
                    break;
                case EnemyVariant.BAT_WINTER:
                    nextWaveBatWinter.SetActive(show);
                    break;
                case EnemyVariant.BAT_DESERT:
                    nextWaveBatDesert.SetActive(show);
                    break;
                case EnemyVariant.SMALLDRAGON_RED:
                    nextWaveSmallDragonRed.SetActive(show);
                    break;
                case EnemyVariant.SMALLDRAGON_WINTER:
                    nextWaveSmallDragonWinter.SetActive(show);
                    break;
                case EnemyVariant.SMALLDRAGON_DESERT:
                    nextWaveSmallDragonDesert.SetActive(show);
                    break;
            }
        }
        
        private TextAnimator textAnimator;
        protected void Awake()
        {
            instance = this;
            shopLeftPanel.SetActive(false);
            shopRightPanel.SetActive(false);
            shopBuyButton.SetActive(false);
            shopSellButton.SetActive(false);
            shopUpgradeButton.SetActive(false);
            GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
            this.gameManager = gameManagerObject.GetComponent<GameManager>();
            this.waveManager = gameManagerObject.GetComponent<WaveManager>();
            this.textAnimator = gameObject.GetComponent<TextAnimator>();
        }
        public void ShowInfoDialoguePanel(bool show)
        {
            this.infoDialoguePanel.SetActive(show);
        }

        public void ShowKingDialoguePanel(bool show)
        {
            this.kingDialoguePanel.SetActive(show);
            this.kingDialoguePanel.transform.Find("Image").gameObject.SetActive(false);
        }

        public void ShowOtherDialoguePanel(bool show)
        {
            this.otherDialoguePanel.SetActive(show);
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
                kingDialoguePanel.transform.Find("ImageAnimation").gameObject.SetActive(true);
                Invoke("HideKingDialoguePanel", dialogueLine.GetTimeToDisappear());
            }
            else if (entity == DialogueEntity.INFO)
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
                        name = "General Palanthor";
                        break;
                    case DialogueEntity.ENGINEER:
                        name = "Army Engineer";
                        break;
                    case DialogueEntity.VILLAGER:
                        name = "Villager";
                        break;
                    case DialogueEntity.GENERAL2:
                        name = "General Ragnar";
                        break;
                    case DialogueEntity.GENERAL3:
                        name = "General Raffa";
                        break;
                    case DialogueEntity.BOSS1:
                        name = "Caildrass, The Clumsy One";
                        break;
                    case DialogueEntity.BOSS2:
                        name = "Onerth, The Strong Minded";
                        break;
                    case DialogueEntity.BOSS3:
                        name = "Dirsy, Icebreath";
                        break;
                    case DialogueEntity.BOSS4:
                        name = "Rimbem, Terror Bringer";
                        break;
                    case DialogueEntity.WIZARD:
                        name = "Wizard Rewind";
                        break;
                }
                otherDialoguePanel.transform.Find("NameHeader").GetComponent<Text>().text = name;
                GameObject entityImageObject = otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).gameObject;
                entityImageObject.SetActive(true);
                otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).Find("ImageAnimation").gameObject.SetActive(true);
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
                {
                    this.otherDialoguePanel.transform.Find(entity).Find("Image").gameObject.SetActive(false);
                    this.otherDialoguePanel.transform.Find(entity).gameObject.SetActive(false);
                }
            }
        }

        public void StopCurrentDialogAnimation()
        {
            if (!DialogueManager.instance.HasDialogueInProgress())
                return;

            DialogueEntity entity = DialogueManager.instance.GetCurrentActiveEntity();
            AudioSource audio;
            if (entity == DialogueEntity.KING)
            {
                audio = kingDialoguePanel.gameObject.GetComponent<AudioSource>();
                kingDialoguePanel.transform.Find("ImageAnimation").gameObject.SetActive(false);
                kingDialoguePanel.transform.Find("Image").gameObject.SetActive(true);

            }
            else if (entity == DialogueEntity.INFO)
            {
                audio = infoDialoguePanel.gameObject.GetComponent<AudioSource>();
            }
            else
            {
                audio = otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).gameObject.GetComponent<AudioSource>();
                otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).Find("ImageAnimation").gameObject.SetActive(false);
                otherDialoguePanel.transform.Find(Enum.GetName(typeof(DialogueEntity), entity)).Find("Image").gameObject.SetActive(true);
            }
            audio.Stop();
        }
    }

}
