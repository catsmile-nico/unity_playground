using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APen_Scoring : MonoBehaviour
{
    //Editor Var
    [SerializeField] GameObject restartBTN = null;
    [SerializeField] GameObject endText = null;
    
    //Start Var
    Text score_textGC = null;

    //Game Var
    ulong score = 0;
    ulong scorePool = 0;
    int shotsTaken = 0;
    bool gameEnd = false;

    void Start()    {
        score_textGC = GetComponent<Text>();
    }

    void Update()    {
        score_textGC.text = score.ToString();
        if (scorePool > 0 && !gameEnd)
            scoreExpenser();
    }

    public void addScore(){
        scorePool+=(ulong)( Mathf.Pow(10, (scorePool.ToString().Length+1)/2) );
    }

    public void addShotCount(){
        shotsTaken+=1;
    }

    public void multiplyScore(int fv_multiplier){
        scorePool += (score*(ulong)(fv_multiplier));
    }

    void scoreExpenser(){
        if (scorePool.ToString().Length > 1) {
            int fasterDivision = 1;
            if (scorePool.ToString().Length > 3)
                fasterDivision = scorePool.ToString().Length/3;
            ulong digitCount = (ulong)( (scorePool.ToString().Length-1) / fasterDivision * 10);
            ulong tv_addScore = (scorePool/digitCount);
            scorePool -= tv_addScore;
            try{
                score = checked(score + tv_addScore);
            } catch (System.OverflowException e) {
                restartLevel();
            }       
            
        }
        else {
            if (scorePool > 0){
                scorePool -= 1;
                score += 1;
            }
        }
    }

    void restartLevel(){
        if (!gameEnd){
            gameEnd = true;
            GameObject newButton = Instantiate(restartBTN) as GameObject;
            newButton.transform.SetParent(GameObject.Find("UI").transform, false);

            GameObject newText = Instantiate(endText) as GameObject;
            newText.transform.SetParent(GameObject.Find("UI").transform, false);

            RectTransform tv_textTransform = newText.GetComponent<RectTransform>();
            tv_textTransform.anchoredPosition = new Vector2(0,0);

            Text tv_endText = newText.GetComponent<Text>();
            tv_endText.text = "You took " + shotsTaken + " shots to exceed the highscore";
        }   
    }

    public bool getGameState(){
        return gameEnd;
    }

}
