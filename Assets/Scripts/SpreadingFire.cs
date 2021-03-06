﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpreadingFire : MonoBehaviour
{
    public GameObject fireGameObject;
    public static Tilemap FireTilemap;
    public TileBase tileBase;
    public NotPlaceAble notBurnable, damageAbleTiles;
    public float chanceToSpread;
    public int spreadRate = 50;
    [Range(0, 1f)] [Tooltip("At 0 The Amount Of Fire Doesn't Change For Each Individual Fire While Higher Numbers Affect Behaviour More Drastically")]
    public float spreadToFireBias = 0.5f;
    public float damage = 5;
    private Vector3Int _maxBorder, _minBorder;
    private int _tick = 1;

    private bool CanSpawn(Vector3Int location)
    {
        return location.x < _maxBorder.x && location.y < _maxBorder.y && location.x >= _minBorder.x && location.y >= _minBorder.y;
    }
    
    private bool IsAvailable(TileBase tileBase1, TileBase tileBase2)
    {
        if (notBurnable.tiles.Any(tile => tile == tileBase1 || tile == tileBase2))
        {
            return false;
        }

        return tileBase2 != null;
    }

    private bool IsDamageAble(TileBase tileBase)
    {
        return damageAbleTiles.tiles.Any(tile => tile == tileBase);
    }

    private void Awake()
    {
        FireTilemap = GetComponent<Tilemap>();
        _maxBorder = PlacingTurret.tilemap.cellBounds.max;
        _minBorder = PlacingTurret.tilemap.cellBounds.min;
        if (FireTilemap.ContainsTile(tileBase))
        {
            var origin = FireTilemap.origin;
            var fireTiles = new List<Vector3Int>();
            for (var i = 0; i < FireTilemap.size.x; i++)
            {
                for (var e = 0; e < FireTilemap.size.y; e++) 
                {
                    var tile = FireTilemap.GetTile(new Vector3Int(origin.x + i, origin.y + e, 0));
                    if (tile == null || tile != tileBase) continue;
                    fireTiles.Add(new Vector3Int(origin.x + i, origin.y + e, 0));
                }
            }

            foreach (var fireTile in fireTiles)
            {
                var fire = Instantiate(fireGameObject, transform);
                fire.transform.position = FireTilemap.CellToWorld(fireTile) + new Vector3(0.5f, 0.5f, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_tick % spreadRate == 0 && FireTilemap.ContainsTile(tileBase))
        {
            var origin = FireTilemap.origin;
            var fireTiles = new List<Vector3Int>();
            for (var i = 0; i < FireTilemap.size.x; i++)
            {
                for (var e = 0; e < FireTilemap.size.y; e++) 
                {
                    var tile = FireTilemap.GetTile(new Vector3Int(origin.x + i, origin.y + e, 0));
                    if (tile == null || tile != tileBase) continue;
                    fireTiles.Add(new Vector3Int(origin.x + i, origin.y + e, 0));
                }
            }

            if (fireTiles.Count == 0)
            {
                FindObjectOfType<Win>(true).gameObject.SetActive(true);
                FindObjectOfType<Win>().DisplayHighScore();
                var gamePlayMusics = FindObjectsOfType<GamePlayMusic>();
                foreach (var music in gamePlayMusics)
                {
                    music.gameObject.SetActive(false);
                }
                Time.timeScale = 0;
                FindObjectOfType<Pause>().cantPause = true;
            }

            foreach (var fireTile in fireTiles)
            {
                var location = new Vector3Int();
                if (Random.Range(0f, 1f) < chanceToSpread / Mathf.Pow(fireTiles.Count, spreadToFireBias))
                {
                    if (!OneAvailableAround(fireTile)) continue;
                    do
                    {
                        location = new Vector3Int(Random.Range(fireTile.x - 1, fireTile.x + 2),
                            Random.Range(fireTile.y - 1, fireTile.y + 2), fireTile.z);
                    } while (!CanSpawn(location) || location.x != fireTile.x && location.y != fireTile.y || !IsAvailable(FireTilemap.GetTile(location), PlacingTurret.tilemap.GetTile(location)));
                    
                    if (IsDamageAble(FireTilemap.GetTile(location)))
                    {
                        var grid = FindObjectOfType<Grid>();
                        var position = FireTilemap.CellToWorld(location);
                        var damageAbles = grid.GetComponentsInChildren<IDamageAble>();
                        if (damageAbles.Where(damageable => position + new Vector3(0.5f, 0.5f, 0) == damageable.GetPos()).Any(damageable => !damageable.TakeDamage(damage)))
                        {
                            continue;
                        }
                    }
                    
                    FireTilemap.SetTile(location, tileBase);
                    var fire = Instantiate(fireGameObject, transform);
                    fire.transform.position = FireTilemap.CellToWorld(location) + new Vector3(0.5f, 0.5f, 0);
                }
            }
        }
        _tick++;
    }

    bool OneAvailableAround(Vector3Int position)
    {
        var positions = new List<Vector3Int>
        {
            position + Vector3Int.down,
            position + Vector3Int.up,
            position + Vector3Int.left,
            position + Vector3Int.right
        };
        return positions.Any(pos => IsAvailable(FireTilemap.GetTile(pos), PlacingTurret.tilemap.GetTile(pos)));
    }
}

