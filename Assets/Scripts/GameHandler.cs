using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{

    public int Score; 
    public bool gameRunning; 
    public bool diffTimerRun;
    public float diffTimer = 0f;

    public TextMeshProUGUI scoreText;
    public int score; 


    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        diffTimerRun = false; 
        diffTimer = 1; 
        score= 0;

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().SetText("Score: " + score.ToString()); 
    //     if(gameRunning == true){
    //         diffTimerRun = true; 
    //     }
    //     else{ 
    //         diffTimerRun = false; 
    //     }
    //     if(diffTimerRun == true){
    //         StartCoroutine(DifficultyScale());
    //     }
    }

    IEnumerator DifficultyScale(){

        diffTimer += 1;
        yield return new WaitForSeconds(1);
        
    }

    public void AddScore(int _score){
        score += _score;
    }
}
