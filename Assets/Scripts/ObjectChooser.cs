using System;
using UnityEngine;

public class ObjectChooser : MonoBehaviour
{
    public Towers towers;
    public GameObject[] previews;

    private void Start()
    {
        foreach (var preview in previews)
        {
            preview.SetActive(false);
        }
        previews[0].SetActive(true);
        FindObjectOfType<PlacingTurret>().currentPlaceable = previews[0];
    }

    public void ChoseObject(int objectToChose)
    {
        FindObjectOfType<PlacingTurret>().placeTurretMode = objectToChose != 2;
        FindObjectOfType<PlacingTurret>().objectToPlace = towers.objects[objectToChose];
        foreach (var preview in previews)
        {
            preview.SetActive(false);
        }
        previews[objectToChose].SetActive(true);
        FindObjectOfType<PlacingTurret>().currentPlaceable = previews[objectToChose];
    }
}
