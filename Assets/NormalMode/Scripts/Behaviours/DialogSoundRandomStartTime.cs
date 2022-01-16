using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSoundRandomStartTime : MonoBehaviour
{
    private AudioSource audio;
    private GameObject imageAnimation;

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();   
        Transform obj = gameObject.transform.Find("ImageAnimation");
        if (obj != null)
            imageAnimation = obj.gameObject;
    }

    private void Update()
    {
        if (imageAnimation != null)
        {
            if (!imageAnimation.activeSelf)
                return;
            if (!audio.isPlaying)
            {
                PlaySoundWithRandomStart();
            }
        } else
        {
            if (!audio.isPlaying)
            {
                PlaySoundWithRandomStart();
            }
        }

    }
    private void PlaySoundWithRandomStart()
    {
        AudioClip clip = audio.clip;
        float startTime = ((float)Random.Range(0, 10) / 10) * clip.length;
        audio.time = startTime;
        audio.Play();
    }
}
