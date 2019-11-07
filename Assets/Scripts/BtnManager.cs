using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour {

    /*  BtnManager handles all of the UI Button navigations
    *   1) New Game
    *   2) High Scores ->  (NOT YET IMPLEMENTED)
    *   3) How to play ->  (NOT YET IMPLEMENTED)
    *   4) Return to Main Menu
    *   6) Exit App
    * 
    *   Future additions? -> Achievments, Level Select
    */

    //  New Game: Load and Initialize level 1
    public void NewGameBtn(string newGameLevel){
        Debug.Log("Loading Level 1.");  
        SceneManager.LoadScene(newGameLevel);
    }


    //  Highscores
    public void HighscoreBtn(string HighscoreLevel){
        Debug.Log("Loading Highscore level.");
        SceneManager.LoadScene(HighscoreLevel);
    }


    //  Return to Main menu
    public void ReturnMainBtn(string MainMenu){
        Debug.Log("Loading Main Menu level.");
        SceneManager.LoadScene(MainMenu);
    }


    //  Exit application
    public void ExitGameBtn(){
        Debug.Log("Exiting application.");
        Application.Quit();
    }

}