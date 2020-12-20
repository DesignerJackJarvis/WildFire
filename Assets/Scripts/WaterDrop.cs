using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDrop : MonoBehaviour
{
    public TileBase baseTile;

    public void Attack(Vector3 position)
    {
        Debug.Log("It's fire");
        var fires = SpreadingFire.FireTilemap.GetComponentsInChildren<Fire>();
        foreach (var fire in fires)
        {
            var cellToWorld = SpreadingFire.FireTilemap.CellToWorld(SpreadingFire.FireTilemap.WorldToCell(position));
            cellToWorld += new Vector3(0.5f, 0.5f,0);
            {
                if (Vector3.Distance(cellToWorld, fire.transform.position) < Mathf.Abs(1.5f))
                {
                    fire.Health -= 1000;
                }
            }
        }
    }
}
