using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public CardManager2 cManager;
    // Start is called before the first frame update
    string buttonText;
      
    //OnHoverEffect for the menu
    public void OnEnterEffect( Text text)
    {
        buttonText = text.text;
        Debug.Log("Cursor Entering " + name + " GameObject");
        text.text = "- "+buttonText+" -";
        text.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    //Detect when Cursor leaves the GameObject
    public void OnExitEffect(Text text)
    {    
        Debug.Log("Cursor Entering " + name + " GameObject");
        text.text = buttonText;
        text.fontStyle = FontStyle.Normal;
    }

    IEnumerator menuDelay(GameObject menu, bool state)
    {
        yield return new WaitForSeconds(0.8f);
        menu.SetActive(state);
    }

    
    public void hideMenu ( GameObject menu)
    {
        StartCoroutine(menuDelay(menu,false));
        
    }

    public void playAnimation (Animator anim)
    {
        anim.SetTrigger("clicked");
    }

    public void newMenu(GameObject newMenu)
    {
        StartCoroutine(menuDelay(newMenu, true));
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void classicModePlay(GameObject gamePanel)
    {
        StartCoroutine(menuDelay(gamePanel, true));        
        cManager.PlayClassic();
    }
}
