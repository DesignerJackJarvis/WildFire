using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTile : MonoBehaviour
{
    public Tile tile;
    private void Start()
    {
        var position = FindObjectOfType<Grid>().WorldToCell(transform.position);
        SpreadingFire.FireTilemap.SetTile(position, tile);
    }
}
