using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AudioClip audioClip;

    Rigidbody2D gc_rigidbody;
    Animator gc_animator;
    Collider2D gc_collider;

    void Awake() {
        gc_rigidbody = GetComponent<Rigidbody2D>();   
        gc_animator = GetComponent<Animator>();
        gc_collider = GetComponent<CircleCollider2D>();
    }

    public void destroyObject() {
        gc_collider.enabled = false;
        gc_animator.SetBool("Alive", false);
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    void Update() {
        gc_rigidbody.velocity = new Vector3(0, -0.25f, 0);
        if (GameObject.Find("Player") != null)
            if ((GameObject.Find("Player").gameObject.transform.position.y - transform.position.y) > 10 || gc_animator.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
                Destroy(this.gameObject);
        if (transform.position.y < -4)
            gc_animator.SetBool("Alive", false);
    }
    
}
