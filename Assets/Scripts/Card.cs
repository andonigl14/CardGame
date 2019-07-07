using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public int gem;     
    public Sprite cardSprite;
    public Texture gemTexture;
    public GameObject CardManager;
    public ColorBlock[] colors;

    public void ClickCard()
    {

        gameObject.GetComponent<Image>().sprite = cardSprite;
        gameObject.transform.GetChild(0).GetComponent<RawImage>().texture = gemTexture;
        if (CardManager.GetComponent<CardManager2>().currentCard.GetComponent<Image>().sprite == cardSprite
            && CardManager.GetComponent<CardManager2>().currentCard.transform.GetChild(0).GetComponent<RawImage>().texture == gemTexture)
        {
            gameObject.GetComponent<Button>().interactable = false;
            CardManager.GetComponent<CardManager2>().RemoveCard(gameObject);
            CardManager.GetComponent<CardManager2>().GenerateMainCard();
        }
        else
        {
            CardManager.GetComponent<CardManager2>().WrongCardCaller(gameObject);
        }
        
        
        
        
        
    }

    public void SetColor()
    {


        gameObject.GetComponent<Button>().colors = ColorBlock.defaultColorBlock; // colors[gem];

    }
}
