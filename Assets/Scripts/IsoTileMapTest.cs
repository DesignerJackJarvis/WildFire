using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour
{
    public Tilemap tileMap;
    public Tile tile;
    public Vector3Int position;

    private void Start()
    {
        Debug.Log(tileMap.size);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tileMap.SetTile(position, tile);
        }
    }
}
