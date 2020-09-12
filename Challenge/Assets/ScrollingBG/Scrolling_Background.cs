using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Background : MonoBehaviour
{
    public Vector2 parallaxMultiplier;

    Transform mainCamera = null;
    Vector3 cameraPrevPos;
    Vector2 textureSize;

    void Start(){
        //parallax
        mainCamera = Camera.main.transform;
        cameraPrevPos = mainCamera.position;

        //infinite bg
        Sprite sprite_GC = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture_GC_F = sprite_GC.texture;
        textureSize.x = texture_GC_F.width / sprite_GC.pixelsPerUnit;
        textureSize.y = texture_GC_F.height / sprite_GC.pixelsPerUnit;  
    }

    void FixedUpdate(){
        //parallax
        transform.position += new Vector3((mainCamera.position.x-cameraPrevPos.x)*parallaxMultiplier.x, 
                                            (mainCamera.position.y-cameraPrevPos.y)*parallaxMultiplier.y);
        cameraPrevPos = mainCamera.position;

        //infinite bg
        if (Mathf.Abs(mainCamera.position.y-transform.position.y) >= textureSize.y)
            transform.position = new Vector3(transform.position.x, mainCamera.position.y+
                                                                    ((mainCamera.position.y-transform.position.y) % textureSize.y));
        if (Mathf.Abs(mainCamera.position.x-transform.position.x) >= textureSize.x)
            transform.position = new Vector3(mainCamera.position.x+
                                                ((mainCamera.position.x-transform.position.x) % textureSize.x), transform.position.y);
    }
}
