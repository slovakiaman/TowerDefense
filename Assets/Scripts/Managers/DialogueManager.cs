using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Serializable]
    public struct EditorDialogue
    {
        public string dialogueName;
        public Dialogue dialogue;
    }
    public EditorDialogue[] editorDialogues;

    private Dictionary<string ,Dialogue> dialogues;

    private Queue<Dialogue> dialoguesToShow;
    private Dialogue dialogueInProgress;

    private void Awake()
    {
        instance = this;
        dialoguesToShow = new Queue<Dialogue>();
        dialogues = new Dictionary<string, Dialogue>();
        for (int i = 0; i < editorDialogues.Length; i++)
        {
            dialogues.Add(editorDialogues[i].dialogueName, editorDialogues[i].dialogue);
        }
    }

    public void ShowDialogue(string key)
    {
        Dialogue dialogue;
        if (!dialogues.TryGetValue(key, out dialogue))
            return;

        dialoguesToShow.Enqueue(dialogue);
    }

    private void Update()
    {
        // if the dialogue in progress has ended, get the next one in queue
        if (dialogueInProgress == null || dialogueInProgress.Update())
        {
            if (dialoguesToShow.Count > 0)
            {
                dialogueInProgress = dialoguesToShow.Dequeue();
                dialogueInProgress.StartDialogue();
            } 
            else
            {
                dialogueInProgress = null;
            }
        }
    }

    public void Reset()
    {
        dialoguesToShow.Clear();
        dialogueInProgress = null;
        UIManager.instance.ShowKingDialoguePanel(false);
        UIManager.instance.HideOtherDialoguePanel();
    }

    public DialogueEntity GetCurrentActiveEntity()
    {
        return dialogueInProgress.GetCurrentDialogueLine().GetDialogueEntity();
    }
}
