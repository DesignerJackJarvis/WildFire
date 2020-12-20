using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FourWayHydrant: MonoBehaviour, IDamageAble
{
    public int attackInterval;
    public float damage;
    private int _tick;
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
                var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                SpreadingFire.FireTilemap.SetTile(location, null);
                Destroy(transform.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_tick % attackInterval == 0)
        {
            List<RaycastHit2D> raycastHit2Ds = new List<RaycastHit2D>();
            raycastHit2Ds.Add(Physics2D.Raycast(transform.position, Vector2.down, range));
            raycastHit2Ds.Add(Physics2D.Raycast(transform.position, Vector2.up, range));
            raycastHit2Ds.Add(Physics2D.Raycast(transform.position, Vector2.right, range));
            raycastHit2Ds.Add(Physics2D.Raycast(transform.position, Vector2.left, range));
            foreach (var raycastHit2D in raycastHit2Ds.Where(raycastHit2D => raycastHit2D.collider != null))
            {
                if (raycastHit2D.collider.TryGetComponent<Fire>(out var fire)) 
                {
                    fire.Health -= damage;
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