using UnityEngine;

public class ObjectChooser : MonoBehaviour
{
    public Towers towers;

    public void ChoseObject(int objectToChose)
    {
        FindObjectOfType<PlacingTurret>().objectToPlace = towers.objects[objectToChose];
    }
}
