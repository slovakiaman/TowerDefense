using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField] private DialogueEntity entity;
    [SerializeField] private string text;
    [SerializeField] private float timeToDisappear;

    private float timer;

    public DialogueEntity GetDialogueEntity()
    {
        return this.entity;
    }

    public string GetText()
    {
        return this.text;
    }

    public float GetTimeToDisappear()
    {
        return this.timeToDisappear;
    }

    /**
     * Updates timer
     * returns true if timer is finished
     * returns false if timer is still counting
     */
    public bool Update()
    {
        if (timer <= 0)
            return true;
        timer -= Time.deltaTime;
        return false;
    }

    public void StartTimer()
    {
        timer = timeToDisappear;
    }
    
}
