using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeGeneration : MonoBehaviour
{
    public NotPlaceAble spawnAbleTiles;
    public int grassToTreeRatio;
    public GameObject tree;
    private int _spawnAbleCount;
    private List<Vector3Int> _spawnAblePlaces = new List<Vector3Int>();
    private List<Vector3Int> _alreadySpawnedPlaces = new List<Vector3Int>();
    private Tilemap _tilemap;

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
                Debug.Log(_spawnAbleCount);
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
}
