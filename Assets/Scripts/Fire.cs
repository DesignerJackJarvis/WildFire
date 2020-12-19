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
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
