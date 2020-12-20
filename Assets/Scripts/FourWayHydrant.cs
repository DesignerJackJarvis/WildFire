using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FourWayHydrant: MonoBehaviour, IDamageAble
{
    public int attackInterval;
    public float damage;
    private int _tick;
    public float range;
    public float health = 50;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private float _time;
    public UnityEvent onDefeat;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            _time = 1;
            if (health <= 0)
            {
                onDefeat.Invoke();
                var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                SpreadingFire.FireTilemap.SetTile(location, null);
                Destroy(transform.gameObject);
            }
        }
    }
    
    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
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
    
    private void Update()
    {
        _spriteRenderer.color = Color.Lerp(_originalColor, Color.red, _time);
        {
            _time -= Time.deltaTime;
        }
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