using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlacingTurret : MonoBehaviour
{
    public NotPlaceAble notPlaceableTiles;
    public GameObject currentPlaceable;
    public TileBase turretTile;
    public Tilemap tileMap;
    public GameObject objectToPlace;
    public Grid grid;
    public static Tilemap tilemap;
    public bool placeTurretMode = true;
    public int money = 25;

    private bool IsAffordable => objectToPlace.GetComponent<Cost>().cost <= money;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

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
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        var position = grid.WorldToCell(worldPoint);
        if (!placeTurretMode)
        {
            if (IsAffordable && tilemap.GetTile(position) != null || SpreadingFire.FireTilemap.GetTile(position) != null &&
                IsAffordable)
            {
                currentPlaceable.SetActive(true);
                currentPlaceable.transform.position = position + new Vector3(0.5f, 0.5f,0);   
            }
            else
                currentPlaceable.SetActive(false);
        }
        else
        {
           if (IsAvailable(tilemap.GetTile(position), SpreadingFire.FireTilemap.GetTile(position)) && IsAffordable)
           {
               currentPlaceable.SetActive(true);
               currentPlaceable.transform.position = position + new Vector3(0.5f, 0.5f,0); 
           }
           else
               currentPlaceable.SetActive(false); 
        }

        if (placeTurretMode && Input.GetMouseButtonDown(0) && !FindObjectOfType<EventSystem>().IsPointerOverGameObject())
        {
            if (IsAvailable(tilemap.GetTile(position), SpreadingFire.FireTilemap.GetTile(position)) && IsAffordable)
            {
                money -= objectToPlace.GetComponent<Cost>().cost;
                var instantiate = Instantiate(objectToPlace, tilemap.transform);
                var cellToWorld = grid.CellToWorld(position);
                SpreadingFire.FireTilemap.SetTile(position, turretTile);
                instantiate.transform.position = cellToWorld + new Vector3(0.5f, 0.5f); 
            }
        }
        
        else if (Input.GetMouseButtonDown(0) && !FindObjectOfType<EventSystem>().IsPointerOverGameObject())
        {
            if (IsAffordable)
            {
                money -= objectToPlace.GetComponent<Cost>().cost;
                objectToPlace.GetComponent<WaterDrop>().Attack(worldPoint);
            }
        }
    }
}
