using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    public GameObject score100;    
    public GameObject scoreDouble;    
    
    private TextMeshProUGUI gc_scoreText; 
    
    int playerScore = 0;
    const int scoreMultiplier = 2; // points multiplied get per UFO
    const int scorePoints = 100; // points player get per asteroid

    
    void Awake() {
        gc_scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        gc_scoreText.text = playerScore.ToString();
    }

    public void addScore(){
        playerScore += scorePoints;
        Instantiate(score100, ( new Vector3(transform.position.x,transform.position.y+10,transform.position.z) ), Quaternion.identity, transform);
    }

    public void doubleScore(){
        playerScore *= scoreMultiplier;
        Instantiate(scoreDouble, ( new Vector3(transform.position.x,transform.position.y,transform.position.z) ), Quaternion.identity, transform);
        
    }

    public void restartLevel(){
        enabled = false;
        gc_scoreText.text = "Final score: " + playerScore.ToString();
        // TODO: set score to center of screen
    }
}
