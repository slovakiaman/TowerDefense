using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField] private DialogueEntity entity;
    [SerializeField] private string text;

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
        float timeToDisappear = (float)text.Length / 14;
        return timeToDisappear >= 1.5f ? timeToDisappear : 1.5f;
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

    public void Start()
    {
        timer = GetTimeToDisappear();
    }
    
}
