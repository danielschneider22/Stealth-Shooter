using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public TextMeshProUGUI textComponent;
    public Image imageComponent;
    public Sprite hoverSprite;
    public Sprite nonHoveredSprite;
    public Sprite clickSprite;
    public Sprite unselectableSprite;
    public bool unselectable;

    public delegate void ButtonClickedAction();
    public event ButtonClickedAction OnButtonClicked;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void setUnselectable()
    {
        unselectable = true;
        imageComponent.sprite = unselectableSprite;
        textComponent.alpha = .34f;
    }
    public void setSelectable()
    {
        unselectable = false;
        imageComponent.sprite = nonHoveredSprite;
        textComponent.alpha = 1f;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(unselectable)
        {
            return;
        }
        audioManager.Play("UIClick", 0);
        OnButtonClicked();
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (unselectable)
        {
            return;
        }
        imageComponent.sprite = clickSprite;
    }

    /* public void OnPointerEnter(PointerEventData eventData)
    {
        if (unselectable)
        {
            return;
        }
        audioManager.Play("UIHover", 0);
        imageComponent.sprite = hoverSprite;
    } */

    public void OnPointerExit(PointerEventData eventData)
    {
        if (unselectable)
        {
            return;
        }
        imageComponent.sprite = nonHoveredSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (unselectable)
        {
            return;
        }
        imageComponent.sprite = nonHoveredSprite;
    }

}
