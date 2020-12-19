using System;
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
    public float chanceToSpread;
    public int spreadRate = 50;
    public float spreadToFireBias = 1000;
    private Vector3Int _maxBorder, _minBorder;
    private int _tick;

    private bool CanSpawn(Vector3Int location)
    {
        return location.x <= _maxBorder.x && location.y <= _maxBorder.y && location.x >= _minBorder.x && location.y >= _minBorder.y;
    }
    private void Awake()
    {
        FireTilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        _maxBorder = PlacingTurret.tilemap.cellBounds.max;
        _minBorder = PlacingTurret.tilemap.cellBounds.min;
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

            foreach (var fireTile in fireTiles)
            {
                var location = new Vector3Int();
                if (Random.Range(0f, 1f) < chanceToSpread / fireTiles.Count + spreadToFireBias)
                {
                    do
                    {
                        location = new Vector3Int(Random.Range(fireTile.x - 1, fireTile.x + 2),
                            Random.Range(fireTile.y - 1, fireTile.y + 2), fireTile.z);
                    } while (!CanSpawn(location) || location.x != fireTile.x && location.y != fireTile.y);

                    if (FireTilemap.GetTile(location) == tileBase) continue;
                    FireTilemap.SetTile(location, tileBase);
                    var fire = Instantiate(fireGameObject, transform);
                    fire.transform.position = FireTilemap.CellToWorld(location) + new Vector3(0.5f, 0.5f, 0);
                    Debug.Log(location);
                }
            }
        }
        _tick++;
    }
}

