using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTile : MonoBehaviour, IDamageAble
{
    public Tile tile;
    public float health = 50;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                PlacingTurret.tilemap.SetTile(location, null);
                Destroy(transform.parent.gameObject);
            }
        }
    }
    private void Start()
    {
        var position = FindObjectOfType<Grid>().WorldToCell(transform.position);
        SpreadingFire.FireTilemap.SetTile(position, tile);
    }

    public bool TakeDamage(float damage)
    {
        if (damage >= Health)
        {
            Health -= damage;
            return true;
        }

        Health -= damage;
        return false;
    }

    public Vector3 GetPos() => transform.position;
}