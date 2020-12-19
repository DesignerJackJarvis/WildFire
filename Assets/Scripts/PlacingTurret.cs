﻿using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacingTurret : MonoBehaviour
{
    public NotPlaceAble notPlaceableTiles;
    public TileBase turretTile;
    public GameObject objectToPlace;
    public Grid grid;
    public Tilemap tilemap;
    public bool placeTurretMode = true;

    private bool IsAvailable(TileBase tileBase1, TileBase tileBase2)
    {
        if (notPlaceableTiles.tiles.Any(tile => tile == tileBase1 || tile == tileBase2))
        {
            return false;
        }

        return tileBase1 != null;
    }
    
    private void Update()
    {
        if (placeTurretMode && Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            var position = grid.WorldToCell(worldPoint);
            if (IsAvailable(tilemap.GetTile(position), SpreadingFire.FireTilemap.GetTile(position)))
            {
                var instantiate = Instantiate(objectToPlace);
                var cellToWorld = grid.CellToWorld(position);
                SpreadingFire.FireTilemap.SetTile(position, turretTile);
                instantiate.transform.position = cellToWorld + new Vector3(0.5f, 0.5f); 
            }
        }
    }
}