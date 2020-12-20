using UnityEngine;

public class ObjectChooser : MonoBehaviour
{
    public Towers towers;

    public void ChoseObject(int objectToChose)
    {
        FindObjectOfType<PlacingTurret>().placeTurretMode = objectToChose != 2;
        FindObjectOfType<PlacingTurret>().objectToPlace = towers.objects[objectToChose];
    }
}
