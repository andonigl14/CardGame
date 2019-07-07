using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager2 : MonoBehaviour
{
    //Second came manager for "choose the same card playstyle"
    //CARD ARRAYS
    public Sprite[] cardSprites;
    public Texture[] gemsSprites;
    public List<GameObject> easyCards;
    //INDEXES
    public GameObject[] difficultCards;
    public GameObject currentCard;  
    public GameObject[] difficultRandomCards;
    public Sprite[] backDrawing;
    public Texture greyGem;
    //EXCLUSIVE RAMDOM UTILITY LISTS
    public List<int> randomChecker = new List<int>();
    public List<GameObject> easyRandomCards;
    List<GameObject> removedCards = new List<GameObject>();
    //INDEXES
    int cardIndex;
    int gemIndex;        
   
    //Classic
    public int currentlevel=0;
    public GameObject endMatchMenu;    
    float trialTime = 0f;
    public Text timeText;
    int timeAux;
    int back;
    
    void Update()
    {
        Timer();        
    }

    //GAMEFLOW
    public void GenerateCards()
    {
        trialTime = 0f;
        endMatchMenu.SetActive(false);
        //Generate r cards
        randomChecker.Clear();
        back = Random.Range(0, 3);
        
        foreach (GameObject g in easyCards)
        {
            gemIndex = Random.Range(0, 5);
            g.transform.GetChild(0).GetComponent<RawImage>().texture = gemsSprites[gemIndex];
            g.GetComponent<Card>().gem = gemIndex;
            g.GetComponent<Card>().gemTexture = g.transform.GetChild(0).GetComponent<RawImage>().texture;

            cardIndex = ExclusiveRandom(0, 99);
           
            g.GetComponent<Image>().sprite = cardSprites[cardIndex];
            g.GetComponent<Card>().cardSprite = cardSprites[cardIndex];
            removedCards.Add(g);

        }
        randomChecker.Clear();

        //Generate main card random array
       
        int index;
          
        for (int i =0; i<8; i++) // we use for instead of foreach to be able to modify the lists
        {
            index = Random.Range(0, removedCards.Count);

            easyRandomCards.Add(removedCards[index]);
            removedCards.RemoveAt(index);
           
        }
        removedCards.Clear();
      
    }

    //Generate exlusive main card choosing between generated cards
    public void GenerateMainCard()
    {
        if (easyRandomCards.Count > 0)
        {
            currentCard.GetComponent<Image>().sprite = easyRandomCards[0].GetComponent<Card>().cardSprite;
            currentCard.transform.GetChild(0).GetComponent<RawImage>().texture = easyRandomCards[0].GetComponent<Card>().gemTexture;
        }
        else
        {
            EndMatch();
        }

    }

    public void HideMainCard()
    {

        currentCard.GetComponent<Image>().sprite = backDrawing[back];
        currentCard.transform.GetChild(0).GetComponent<RawImage>().texture = greyGem;
    }

    //GAMEMODES
    public void PlayClassic()
    {
        GenerateCards();
        HideMainCard();
        StartCoroutine(HideCards());
       
    }

    public void ActivateCards( bool activation)
    {
        foreach (GameObject g in easyRandomCards)
        {
            g.GetComponent<Button>().interactable = activation;
        }
    }
    public void RemoveCard(GameObject card)
    {
        foreach ( GameObject C in easyRandomCards)
        {
            if ( C == card)
            {
                removedCards.Add(C);
            }
        }
        easyRandomCards.Remove(removedCards[0]);
        removedCards.Clear();

    }

    IEnumerator HideCards()
    {
        Debug.Log("hiding cards");
        yield return new WaitForSeconds(5.0f);
        
        foreach (GameObject g in easyCards)
        {
            g.GetComponent<Image>().sprite = backDrawing[back];
            g.transform.GetChild(0).GetComponent<RawImage>().texture = greyGem;
            g.GetComponent<Button>().interactable = true;
        }
        GenerateMainCard();
    }
    //stop playing
    void EndMatch()
    {
        Debug.Log("match ended");
        currentlevel++;
        endMatchMenu.SetActive(true);
    }
       

    public void WrongCardCaller( GameObject card)
    {
        StartCoroutine(WrongCard(card));
    }

    IEnumerator WrongCard(GameObject card)
    {
        ActivateCards(false);
        yield return new WaitForSeconds(2.0f);
        card.GetComponent<Image>().sprite = backDrawing[back];
        card.transform.GetChild(0).GetComponent<RawImage>().texture = greyGem;
        ActivateCards(true);
    }
    //UTILITIES

    void Timer()
    {
        trialTime += Time.deltaTime;
        timeAux = (int)trialTime;
        timeText.text = timeAux.ToString();
    }
      
    //exclusive random list
    int ExclusiveRandom(int min, int max)
    {
        //  Debug.Log("-----ENTRO EN RANDOM-----");
        int result;
        result = Random.Range(min, max);

        while(randomChecker.Contains(result))
        {
            result = Random.Range(min, max);
        }
            randomChecker.Add(result);
     
        return result;

    }
}
