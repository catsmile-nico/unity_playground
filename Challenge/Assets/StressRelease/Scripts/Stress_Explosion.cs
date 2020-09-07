using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress_Explosion : MonoBehaviour
{
    Animator anim;

    public AudioClip audioClip;
    // AudioSource audio;
    
    void Start()    {
        anim = GetComponent<Animator>();
        //Explosion noise control   
        AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.5f + (1%GameObject.FindGameObjectsWithTag("Explosion").Length/1200f));
        // Mathf.Abs(GameObject.FindGameObjectsWithTag("Explosion").Length/100)
        // audio = GetComponent<AudioSource>();
        // audio.Play();
    }

    // Update is called once per frame
    void Update()    {
        // if (GameObject.FindGameObjectsWithTag("Explosion").Length > 20) 
        //     audio.mute = true;
        // else
        //     audio.mute = false;
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            Destroy(this.gameObject);
    }
}
