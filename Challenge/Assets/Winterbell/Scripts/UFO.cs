using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public AudioClip audioClip;

    Rigidbody2D gc_rigidbody;
    Animator gc_animator;
    Collider2D gc_collider;

    bool facingRight = true;
    bool hit = false;
    float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start() {
        gc_rigidbody = GetComponent<Rigidbody2D>();   
        gc_animator = GetComponent<Animator>();
        gc_collider = GetComponent<CircleCollider2D>();
    }

    public void destroyObject() {
        gc_collider.enabled = false;
        gc_animator.SetBool("Hit", true);
        hit = true;
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    // Update is called once per frame
    void Update() {
        if (!hit){
            gc_rigidbody.velocity = new Vector3(moveSpeed, -0.25f, 0);
        
            if (facingRight && transform.position.x > 10.5) {
                transform.localScale = new Vector3(-1,1,1);
                facingRight = false;
                moveSpeed = -5.0f;
            } else if (!facingRight && transform.position.x < -10.5) {
                transform.localScale = new Vector3(1,1,1);
                facingRight = true;
                moveSpeed = 5.0f ;
            }
        } else 
            gc_rigidbody.velocity = new Vector3(0, -5f, 0);

        if (GameObject.Find("Player") != null)
            if ((GameObject.Find("Player").gameObject.transform.position.y - transform.position.y) > 10)
                Destroy(this.gameObject);
    }   
}
