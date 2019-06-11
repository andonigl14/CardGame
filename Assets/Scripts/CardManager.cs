using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Sprite[] cardSprites;
    public Texture[] gemsSprites;
    public GameObject[] Cards;
    public int[] finalNumbers;    
    public Text ResultText;
    public Color answerColor;
    List<int> randomChecker = new List<int>();
    int cardIndex;
    int gemIndex;    
    int finalResult=0;
    public int clickedCards;
    public int myAnwer;
    string gameMode ="classic";

    //classic
    public int currentlevel=0;
    public int maxlevels=10;
    //survial
    bool isCorrect=true;
    //time trial
    float trialTime = 300.0f;
    public Text timeText;
    int timeAux;

    void Update()
    {
        Timer();
    }
      
    //GAMEFLOW
    public void generateCards()
    {
        randomChecker.Clear();       
        foreach (GameObject g in Cards)
        {
            gemIndex = Random.Range(0, 5);
            g.transform.GetChild(1).GetComponent<RawImage>().texture = gemsSprites[gemIndex];
            g.GetComponent<Card>().gem = gemIndex;
            g.GetComponent<Card>().setColor();

            cardIndex = ExclusiveRandom(0, 10);
            g.GetComponent<Image>().sprite = cardSprites[cardIndex];
            g.GetComponent<Card>().number = cardIndex;
            g.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = cardIndex.ToString();         

        }

        randomChecker.Clear();

        for (int i = 0; i < 4; i++)
        {
            int position = ExclusiveRandom(0, 4);

            int gemedNumber = ApplyGem(i, Cards[position].GetComponent<Card>().number, Cards[position].GetComponent<Card>().gem);
            Debug.Log("Genero el: " + position + " y su gemed number es" + gemedNumber);
            finalNumbers[position] = gemedNumber;
            Cards[position].GetComponent<Card>().gemedNumber = gemedNumber;
            //  Debug.Log("Añado: " + gemedNumber + " de " + position);

        }

        randomChecker.Clear();

        foreach (int n in finalNumbers)
        {
            //  Debug.Log("Sumo:" +n);
            finalResult += n;
        }

        ResultText.text = finalResult.ToString();

    }

    public int ApplyGem(int position, int number, int gem)
    {

        int finalNumber = number;
        switch (gem)
        {
            case 0: //BLUE
                if (position == 0)
                    finalNumber = 1;
                if (position == 1)
                    finalNumber = 2;
                if (position == 2)
                    finalNumber = 3;
                if (position == 3)
                    finalNumber = 4;
                break;
            case 1://GREEN
                //it does nothing
                break;
            case 2://PINK
                if (position == 0 || position == 1)
                {
                    finalNumber = finalNumber - 10;
                }
                else
                {
                    finalNumber = finalNumber + 10;
                }
                break;
            case 3://RED
                if (position == 0 || position == 2)
                {
                    finalNumber = -finalNumber;
                }
                else
                {
                    finalNumber = 0;
                }
                break;
            case 4://YELLOW
                if (position == 1 || position == 3)
                {
                    finalNumber = finalNumber * 2;
                }
                else
                {
                    finalNumber = finalNumber * 2;
                    finalNumber = -finalNumber;
                }
                break;

            default:
                Debug.Log("Gem Error");
                break;
        }

        return finalNumber;
    }


    public IEnumerator checkAnswer()
    {
        if (myAnwer == finalResult)
        {
            ResultText.color = Color.green;
            currentlevel++;
        }
        else
        {
            ResultText.color = Color.red;
            isCorrect = false;
        }
        yield return (new WaitForSeconds(3));
        switch (gameMode)
        {

            case "classic":
                if (currentlevel >= maxlevels)
                    endMatch();
                else
                    reset();
                break;

            case "survival":
                if (isCorrect)
                    reset();
                else
                    endMatch();
                break;

            case "timetrial":
                break;

            default:
                Debug.Log("GameMode Error!");
                break;

        }
        yield return null;
    }

    public void reset()
    {
        foreach (GameObject g in Cards)
        {
            g.GetComponent<Card>().clickedNumberText.gameObject.SetActive(false);
            g.GetComponent<Button>().interactable = true;

        }
        clickedCards = 0;
        myAnwer = 0;
        finalResult = 0;
        currentlevel++;
        ResultText.color = answerColor;
        generateCards();

    }

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
