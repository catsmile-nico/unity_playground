using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    
    // float scrollSpeed = 0.1f;
    SpriteRenderer spriterender_GC;
    Material mat_GC; 
    Vector2 offset;

    void Start() {
        spriterender_GC = GetComponent<SpriteRenderer> ();
        mat_GC = spriterender_GC.material;
        offset = mat_GC.mainTextureOffset;
    }

    void Update() {
        offset.x += Time.deltaTime;
        mat_GC.mainTextureOffset = offset;
        // TODO: make proper bg that scales with game (instead of 1 static bg)   
        // float offset = Time.time * scrollSpeed;
        // rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
        // Debug.Log(offset);
    }

    
}
