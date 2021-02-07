using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizeLevel : MonoBehaviour
{
    public Grid[] grid;
    public static bool restart;
    private static int _currentLevel;

    private void Awake()
    {
        if (!restart)
            _currentLevel = Random.Range(0, grid.Length);
        Instantiate(grid[_currentLevel]);
        Time.timeScale = 1;
        restart = false;
    }
}
