using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public int gem;
    public int number;
    public int gemedNumber;
    public int clickedNumber;
    public int position;
    public Text clickedNumberText;
    bool clicked = false;
    public Sprite cardSprite;
    public GameObject CardManager;
    public ColorBlock[] colors;

    public void ClickCard()
    {
        Debug.Log("Wola");
        position = CardManager.GetComponent<CardManager>().clickedCards;
        clickedNumber = CardManager.GetComponent<CardManager>().ApplyGem(position, number, gem);
        CardManager.GetComponent<CardManager>().clickedCards++;
        clickedNumberText.gameObject.SetActive(true);
        clickedNumberText.text = clickedNumber.ToString();
        CardManager.GetComponent<CardManager>().myAnwer += clickedNumber;
        gameObject.GetComponent<Button>().interactable = false;
        if (CardManager.GetComponent<CardManager>().clickedCards == 4)
        {
            StartCoroutine( CardManager.GetComponent<CardManager>().checkAnswer());
        }
    }

    public void setColor()
    {


        gameObject.GetComponent<Button>().colors = ColorBlock.defaultColorBlock; // colors[gem];

    }
}
