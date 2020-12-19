using UnityEngine;

public class StationaryTower : MonoBehaviour, IDamageAble
{
    public int attackInterval;
    public float damage;
    private int _tick;
    public Vector3 direction;
    public float range;
    public float health = 50;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
            {
                //var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                //PlacingTurret.tilemap.SetTile(location, null);
                Destroy(transform.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_tick % attackInterval == 0)
        {
            var ray = Physics2D.Raycast(transform.position, direction, range);
            GetComponentInChildren<LineRenderer>().SetPositions(new []{transform.position, transform.position + direction * range});
            {
                if (ray.collider != null)
                {
                    if (ray.collider.TryGetComponent<Fire>(out var fire))
                    {
                        fire.Health -= damage;
                    }
                }
            }
        }
        _tick++;
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
