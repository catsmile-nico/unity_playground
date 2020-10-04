using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemaps_Player : MonoBehaviour
{
    public Tile tile; //drawable tiles
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()    {
        if (Input.GetMouseButton(0)){
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (tilemap.GetTile(tilemap.WorldToCell(position)) == null)
                tilemap.SetTile(tilemap.WorldToCell(position), tile);
        }

        if (Input.GetMouseButtonDown(1)){
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tilemap.SetTile(tilemap.WorldToCell(position), null);
        }
    }

}
