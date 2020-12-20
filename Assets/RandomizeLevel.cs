using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizeLevel : MonoBehaviour
{
    public Grid[] grid;

    private void Awake()
    {
        var randomNum = Random.Range(0, grid.Length);
        Instantiate(grid[randomNum]);
    }
}
