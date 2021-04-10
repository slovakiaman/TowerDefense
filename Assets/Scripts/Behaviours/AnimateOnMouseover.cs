using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimateOnMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Animator controller;

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.SetBool("animate", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.SetBool("animate", false);
    }

}
