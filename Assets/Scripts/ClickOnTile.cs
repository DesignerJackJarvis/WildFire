using System;
using UnityEngine;

public class ClickOnTile : MonoBehaviour
{
    public GameObject whatToInstantiate;
    public Grid grid;
    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            var position = grid.WorldToCell(worldPoint);
            var instantiate = Instantiate(whatToInstantiate);
            instantiate.transform.position = grid.CellToWorld(position);
        }*/
    }
}
