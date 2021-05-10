using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalUIManager : UIManager
{
    [Header("DialoguePanels")]
    [SerializeField] private GameObject infoDialoguePanel;
    [SerializeField] private GameObject kingDialoguePanel;
    [SerializeField] private GameObject otherDialoguePanel;

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
        this.GameManager = (NormalGameManager)GameManagerObject.GetComponent<NormalGameManager>();
        this.WaveManager = (NormalWaveManager)GameManagerObject.GetComponent<NormalWaveManager>();
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
