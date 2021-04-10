using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class Dialogue: ScriptableObject
{
    [SerializeField]
    private List<DialogueLine> lines;

    private DialogueLine actualLine;
    private int actualLineIndex;

    public List<DialogueLine> GetLines()
    {
        return this.lines;
    }

    /**
     * Updates dialogue lines
     * returns true if all dialogue lines have been shown
     * returns false if dialogue lines are still showing
     */
    public bool Update()
    {
        //if the dialogue line has finished and there are more dialogue lines, get the next one, else return true
        if (actualLine.Update())
            if (actualLineIndex + 1 < lines.Count)
            {
                actualLineIndex++;
                actualLine = lines[actualLineIndex];
                actualLine.StartTimer();
                UIManager.instance.ShowDialogueLine(actualLine);
            }
            else
                return true;

        return false;
    }

    public void StartDialogue()
    {
        actualLine = lines[0];
        actualLineIndex = 0;
        actualLine.StartTimer();
        UIManager.instance.ShowDialogueLine(actualLine);
    }
}
