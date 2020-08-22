using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject UFO;
    
    private int difficultyScale = 1; 
    private int birdCounter = 0; // every 25 spawn bird instead of asteroid
        
    // private float previousX; // TODO: pseudo random to make asteroids spawn in staircase pattern 
    private float previousY = -3.5f;
    private float asteroidGap = 2.5f;
        
    // Start is called before the first frame update
    void Start() {
        for(int i=0; i<10; i++)
            spawnAsteroids();        
    }

    void spawnAsteroids() {
        float randomX = Random.Range(-10.0f, 10.0f);
        if (birdCounter < 25) {
            GameObject asterObj = Instantiate(asteroid, (new Vector3(randomX,previousY+=asteroidGap,0)), Quaternion.identity);
            // TODO: decrease asteroid size overtime; include minimum size limit
            //asterObj.transform.localScale = new Vector3(asterObj.transform.localScale.y/difficultyScale,asterObj.transform.localScale.y/2,asterObj.transform.localScale.y/2);
            birdCounter++;
        } else {
            Instantiate(UFO, (new Vector3(randomX,previousY+=asteroidGap,0)), Quaternion.identity);
            birdCounter = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameObject.Find("Player") != null)
            if (Mathf.Abs(GameObject.Find("Player").gameObject.transform.position.y - previousY) < 10)
                spawnAsteroids();    
    
        // TODO: score system
        // TODO: restart menu
        


    }
}
