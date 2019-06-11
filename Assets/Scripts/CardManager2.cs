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
    public GameObject[] EasyCards;
    public GameObject[] DifficultCards;
    public GameObject CurrentCard;
    public GameObject[] EasyRandomCards;
    public GameObject[] DifficultRandomCards;

    //EXCLUSIVE RAMDOM UTILITY LIST
    List<int> randomChecker = new List<int>();
   
    //INDEXES
    int cardIndex;
    int gemIndex;        
    int currentCardIndex;

    //GAMEMODES
    string gameMode = "classic";

    //Classic
    public int currentlevel=0;
    public int maxlevels=10;

    //Survial
    bool isCorrect=true;

    //Time trial
    float trialTime = 300.0f;
    public Text timeText;
    int timeAux;
    
    void Update()
    {
        Timer();

        //DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            generateCards();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            generateMainCard();
        }
    }

    //GAMEFLOW
    public void generateCards()
    {
        //Generate lvl cards
        randomChecker.Clear();
        foreach (GameObject g in EasyCards)
        {
            gemIndex = Random.Range(0, 5);
            g.transform.GetChild(0).GetComponent<RawImage>().texture = gemsSprites[gemIndex];
            g.GetComponent<Card>().gem = gemIndex;


            cardIndex = ExclusiveRandom(0, 99);
            g.GetComponent<Image>().sprite = cardSprites[cardIndex];
            g.GetComponent<Card>().cardSprite = cardSprites[cardIndex];

        }
        randomChecker.Clear();

        //Generate main card random array
       for (int i = 0; i<8; i++)
        {

          EasyRandomCards[i] = EasyCards[1];

       }
        randomChecker.Clear();
        currentCardIndex = 0;
    }

    //Generate exlusive main card choosing between generated cards
    public void generateMainCard()
    {
        CurrentCard.GetComponent<Image>().sprite = EasyRandomCards[currentCardIndex].GetComponent<Image>().sprite;
        currentCardIndex++;

    }
      

    //generate new lvl
    public void reset()
    {
        foreach (GameObject g in EasyCards)
        {
            g.GetComponent<Card>().clickedNumberText.gameObject.SetActive(false);
            g.GetComponent<Button>().interactable = true;

        }
        currentlevel++;       
        generateCards();

    }

    //stop playing
    void endMatch()
    {

    }


    //GAMEMODES
    public void playClassic()
    {
        currentlevel++;
        maxlevels = 10;
        trialTime = 0.0f;
        
        if (currentlevel >= maxlevels)
            endMatch();
        else
            generateCards();
    }







    //UTILITIES

    void Timer()
    {
        if (gameMode == "timetrial")
        {
            if (trialTime <= 0)
                trialTime -= Time.deltaTime;

            timeAux = (int)trialTime;
        }

        if (gameMode == "classic")
        {
            trialTime += Time.deltaTime;
            timeAux = (int)trialTime;
        }

        timeText.text = timeAux.ToString();
    }
      
    //exclusive random list
    int ExclusiveRandom(int min, int max)
    {
        //  Debug.Log("-----ENTRO EN RANDOM-----");
        int result;
        result = Random.Range(min, max);

        while (randomChecker.Contains(result))
        {
            result = Random.Range(min, max);
        }
        randomChecker.Add(result);

        // Debug.Log("Utimo result:" + result);
        // Debug.Log("-----Salgo EN RANDOM-----");
        return result;

    }
}
