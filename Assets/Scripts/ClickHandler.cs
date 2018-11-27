using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour,IPointerClickHandler {

    Game game;

    private void Start()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        game = GameManager.GetComponent<Game>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Image img = this.GetComponent<Image>();
        if (img.color == Color.black)
        {
            img.color = Color.white;
            img.sprite = game.playerRefImage.sprite;
            game.changeTurn();
        }
    }
}
