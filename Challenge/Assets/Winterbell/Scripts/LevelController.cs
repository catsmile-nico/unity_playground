using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Game
    public GameObject asteroid;
    public GameObject UFO;

    // UI
    public GameObject restartButton;
    
    private int birdCounter = 0; // every 25 spawn bird instead of asteroid
        
    // private float previousX; // TODO: pseudo random to make asteroids spawn in staircase pattern 
    private float previousY = -3.5f;
    private float asteroidGap = 2.5f;
    private float difficultyScale = 1.1f; //1.1 is size of character
    private float difficultyCap = 2.3f;
        
    // Start is called before the first frame update
    void Start() {
        // fill screen with asteroid when game starts
        for(int i=0; i<10; i++)
            spawnAsteroids();        
    }

    void spawnAsteroids() {
        // random asteroid spawn X coord
        float randomX = Random.Range(-10.0f, 10.0f);
        
        if (birdCounter < 25) {
            // spawn asteroid
            GameObject asterObj = Instantiate(asteroid, (new Vector3(randomX,previousY+=asteroidGap,0)), Quaternion.identity);
            
            // decrease asteroid size overtime; include minimum size limit
            asterObj.transform.localScale = new Vector3(asterObj.transform.localScale.x/difficultyScale,asterObj.transform.localScale.y/difficultyScale,asterObj.transform.localScale.z);
            birdCounter++;
        } else {
            // spawn UFO bonus every 25 asteroid
            Instantiate(UFO, (new Vector3(randomX,previousY+=asteroidGap,0)), Quaternion.identity);
            birdCounter = 0;

            // decrease asteroid size overtime; include minimum size limit
            if (difficultyScale < difficultyCap)
                difficultyScale += 0.1f;
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Player") != null) {
            GameObject player = GameObject.Find("Player");
            
            //Spawn Asteroids
            if (Mathf.Abs(player.gameObject.transform.position.y - previousY) < 10)
                spawnAsteroids();    

                  
        }        
    }

    // restart menu
    public void restartLevel(){
        GameObject newButton = Instantiate(restartButton) as GameObject;
        newButton.transform.SetParent(GameObject.Find("UI").transform, false);
        GameObject.Find("Scoreboard").GetComponent<Scoring>().restartLevel();
    }


}
