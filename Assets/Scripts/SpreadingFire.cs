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
    public Vector3Int maxBorder, minBorder;
    private int _tick;

    private bool CanSpawn(Vector3Int location)
    {
        return location.x <= maxBorder.x && location.y <= maxBorder.y && location.x >= minBorder.x && location.y >= minBorder.y;
    }
    private void Start()
    {
        FireTilemap = GetComponent<Tilemap>();
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
                    if (tile == null) continue;
                    fireTiles.Add(new Vector3Int(origin.x + i, origin.y + e, 0));
                }
            }

            foreach (var location in fireTiles.Select(fireTile => new Vector3Int(Random.Range(fireTile.x - 1, fireTile.x + 2),
                Random.Range(fireTile.y - 1, fireTile.y + 2), fireTile.z)))
            {
                if (Random.Range(0f, 1f) < chanceToSpread / fireTiles.Count + spreadToFireBias && CanSpawn(location))
                {
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

