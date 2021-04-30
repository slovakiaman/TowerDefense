using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    private Text textObject;
    private string text;
    private int charIndex;
    [SerializeField] private float charCountdown = 0.3f;
    private float countdown;

    public void AnimateDialogueLine(Text textObject, string text)
    {
        this.textObject = textObject;
        textObject.text = "";
        this.text = text;
        this.charIndex = 0;
        this.countdown = 0;
    }

    void Update()
    {
        if (textObject == null)
            return;
        if (countdown <= 0)
        {
            if (charIndex < text.Length)
            {
                textObject.text += text[charIndex];
                countdown = charCountdown;
                charIndex++;
            } else
            {
                UIManager.instance.StopCurrentDialogAnimation();
                Reset();
            }
        }
        countdown -= Time.deltaTime;
    }

    private void Reset()
    {
        this.textObject = null;
        this.text = "";
        this.countdown = 0;
        this.charIndex = 0;
    }


}
