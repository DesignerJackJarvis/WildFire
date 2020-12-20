using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TreeGeneration : MonoBehaviour
{
    public NotPlaceAble spawnAbleTiles;
    public int grassToTreeRatio;
    public GameObject tree;
    public Tile treeTile;
    public event Action<int> ONTreeDead;
    private int _spawnAbleCount;
    private List<Vector3Int> _spawnAblePlaces = new List<Vector3Int>();
    public List<Vector3Int> _alreadySpawnedPlaces = new List<Vector3Int>();
    private Tilemap _tilemap;

    public int TreesRemaining
    {
        get
        {
            if (_alreadySpawnedPlaces.Count == _spawnAbleCount / grassToTreeRatio)
            {
                return _alreadySpawnedPlaces.Count;
            }
            return _alreadySpawnedPlaces.Count - 1;
        }
    }

    bool SpawnAble(Vector3Int position)
    {
        if (_alreadySpawnedPlaces.Count == 0)
            return true;
        foreach (var alreadySpawnedPlace in _alreadySpawnedPlaces)
        {
            if (Vector3Int.Distance(alreadySpawnedPlace, position) < Mathf.Abs(1.5f))
            {
                return false;
            }

            var location = SpreadingFire.FireTilemap.WorldToCell(_tilemap.CellToWorld(position));
            if (SpreadingFire.FireTilemap.GetTile(location) != null)
            {
                return false;
            }
        }

        return true;
    }
    private void Start()
    {
        _tilemap = PlacingTurret.tilemap;
        for (var i = 0; i < _tilemap.size.x; i++)
        {
            for (var e = 0; e < _tilemap.size.y; e++) 
            {
                var tile = _tilemap.GetTile(new Vector3Int(_tilemap.origin.x + i, _tilemap.origin.y + e, 0));
                if (tile == null || tile != spawnAbleTiles.tiles[0] && tile != spawnAbleTiles.tiles[1]) continue;
                _spawnAbleCount++;
                _spawnAblePlaces.Add(new Vector3Int(_tilemap.origin.x + i, _tilemap.origin.y + e, 0));
            }
        }

        for (var i = 0; i < _spawnAbleCount / grassToTreeRatio; i++)
        {
            var newTree = Instantiate(tree, transform);
            var position = new Vector3Int();
            do
            {
                position = _spawnAblePlaces[Random.Range(0, _spawnAblePlaces.Count)];
            } while (!SpawnAble(position));
            
            newTree.transform.position = position + new Vector3(0.5f, 0.5f, 0);
            _alreadySpawnedPlaces.Add(position);
        }
    }

    public void TreeDefeat()
    {
        for (int i = 0; i < _alreadySpawnedPlaces.Count; i++)
        {
            var tile = SpreadingFire.FireTilemap.GetTile(_alreadySpawnedPlaces[i]);
            if (tile != treeTile)
            {
                _alreadySpawnedPlaces.RemoveAt(i);
            }
        }
        ONTreeDead?.Invoke(_alreadySpawnedPlaces.Count - 1);

        if (_alreadySpawnedPlaces.Count == 1)
        {
            FindObjectOfType<Lose>(true).gameObject.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<Pause>().cantPause = true;
        }
    }
}
