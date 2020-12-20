using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float health = 10;

    public float Health
    {
        get => health;
        set
        {
            health = value;
            Debug.Log("Fire was hit");
            if (health <= 0)
            {
                var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                SpreadingFire.FireTilemap.SetTile(location, null);
                Debug.Log("Fire was put out");
                var random = Random.Range(0, 1.0f);
                if (random < 0.05f)
                {
                    FindObjectOfType<PlacingTurret>().money += 50;
                }
                else if (random < 0.15f)
                    FindObjectOfType<PlacingTurret>().money += 25;
                else
                    FindObjectOfType<PlacingTurret>().money += 5;
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
