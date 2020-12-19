using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpreadingFire : MonoBehaviour
{
    public GameObject fireGameObject;
    private Tilemap _fireTilemap;
    public TileBase tileBase;
    public float chanceToSpread;
    public int spreadRate = 50;
    public float spreadToFireBias = 1000;
    public Vector3Int maxBorder, minBorder;
    private int _tick;

    private bool CanSpawn(Vector3Int location)
    {
        return location.x <= maxBorder.x && location.y <= maxBorder.y && location.x >= minBorder.x && location.y >= minBorder.y;
    }
    private void Start()
    {
        _fireTilemap = GetComponent<Tilemap>();
    }

    private void FixedUpdate()
    {
        if (_tick % spreadRate == 0 && _fireTilemap.ContainsTile(tileBase))
        {
            var origin = _fireTilemap.origin;
            var fireTiles = new List<Vector3Int>();
            for (var i = 0; i < _fireTilemap.size.x; i++)
            {
                for (var e = 0; e < _fireTilemap.size.y; e++) 
                {
                    var tile = _fireTilemap.GetTile(new Vector3Int(origin.x + i, origin.y + e, 0));
                    if (tile == null) continue;
                    fireTiles.Add(new Vector3Int(origin.x + i, origin.y + e, 0));
                }
            }

            foreach (var location in fireTiles.Select(fireTile => new Vector3Int(Random.Range(fireTile.x - 1, fireTile.x + 2),
                Random.Range(fireTile.y - 1, fireTile.y + 2), fireTile.z)))
            {
                if (Random.Range(0f, 1f) < chanceToSpread / fireTiles.Count + spreadToFireBias && CanSpawn(location))
                {
                    _fireTilemap.SetTile(location, tileBase);
                    var fire = Instantiate(fireGameObject);
                    fire.transform.position = _fireTilemap.CellToWorld(location) + new Vector3(0.5f, 0.5f, 0);
                    Debug.Log(location);
                }
            }
        }

        _tick++;
    }
}

