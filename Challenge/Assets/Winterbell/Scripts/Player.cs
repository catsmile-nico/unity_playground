using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D gc_rigidbody;
    Animator gc_animator;
    
    Rect screenRect;

    // SerializeFields
    [SerializeField] float moveSpeed = 75;
    [SerializeField] float jumpPower = 600;

    // Values
    Vector3 mousePosition;

    // States
    bool firstScore = false;
    bool dead = false;

    void Awake() {
        gc_rigidbody = GetComponent<Rigidbody2D>();   
        gc_animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("Asteroid")){
            col.gameObject.GetComponent<Asteroid>().destroyObject();
            firstScore = true;
            jump();
            GameObject.Find("Scoreboard").GetComponent<Scoring>().addScore();
        } 

        if (col.gameObject.name.Contains("UFO")){
            col.gameObject.GetComponent<UFO>().destroyObject();
            jump();
            GameObject.Find("Scoreboard").GetComponent<Scoring>().doubleScore();
        } 
    }

    void OnCollisionEnter2D(Collision2D col) {

        //Reset game
        if (col.gameObject.name.Contains("Ground") && firstScore && !dead){
            //Destroy(this.gameObject);
            //SceneManager.LoadScene("Winterbell-Clone"); //FIXED: fix null exception on load
            GameObject.Find("LevelController").GetComponent<LevelController>().restartLevel();   
            dead = true;
        }
    }

    void jump() {
        gc_rigidbody.velocity = new Vector2(gc_rigidbody.velocity.x, 0); // reset velocity on each jump
        gc_rigidbody.AddForce(new Vector2(0f,jumpPower));
    }

    // Update is called once per frame
    void Update() {

        // if (Input.GetButtonDown("Jump") && !firstScore) 
        // FIXED: disable after first jump 
        if (Input.GetMouseButtonDown(0) && !firstScore && transform.position.y <= -5.04f) 
            jump();

        // Out of screen mechanics
        // TODO: if hit y threshold dont update x to extreme positions
        if (screenRect.Contains(Input.mousePosition))
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        else if (mousePosition.x > 0 || 10.5 < mousePosition.x)
            mousePosition.x = 10.5f;
        else if (mousePosition.x < 0 || -10.5 > mousePosition.x)
            mousePosition.x = -10.5f;
        
        // Follow mouse movement
        float move = Mathf.Abs(transform.position.x - mousePosition.x);
        if (move > 0.05f) {
            Vector2 direction = (mousePosition - transform.position).normalized;
            gc_rigidbody.velocity = new Vector2(direction.x*moveSpeed, gc_rigidbody.velocity.y);
        } else {
            gc_rigidbody.velocity = new Vector2(0, gc_rigidbody.velocity.y);
        }

    }
    
}
