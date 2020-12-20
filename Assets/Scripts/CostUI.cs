using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour
{
    private PlacingTurret _placingTurret;
    private Text _text;
    private void Start()
    {
        _placingTurret = FindObjectOfType<PlacingTurret>();
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text =  _placingTurret.money.ToString();
    }
}
