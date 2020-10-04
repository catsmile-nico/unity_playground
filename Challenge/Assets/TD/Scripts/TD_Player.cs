using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class TD_Player : MonoBehaviour
{
    public Tile tile; //drawable tiles
    public Tilemap tilemap;

    //public PathFinder pathFinder = null;

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
            AstarPath.active.Scan();
        }
    }

}
