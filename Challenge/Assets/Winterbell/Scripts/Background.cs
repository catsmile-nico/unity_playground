using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public AudioSource audioSource;

    float scrollSpeed = 0.1f;
    SpriteRenderer rend;

    void Start() {
        rend = GetComponent<SpriteRenderer> ();
        audioSource.Play();
    }

    void Update() {
        // TODO: make proper bg that scales with game (instead of 1 static bg)   
        // float offset = Time.time * scrollSpeed;
        // rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        // Debug.Log(offset);
    }

    
}
