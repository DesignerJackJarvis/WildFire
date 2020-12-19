using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeTile : MonoBehaviour, IDamageAble
{
    public Tile tile;
    public float health = 50;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private float _time;
    public float Health
    {
        get => health;
        set
        {
            health = value;
            _time = 1;
            if (health <= 0)
            {
                //var location = SpreadingFire.FireTilemap.WorldToCell(transform.position - new Vector3(0.5f, 0.5f, 0));
                //PlacingTurret.tilemap.SetTile(location, null);
                Destroy(transform.gameObject);
            }
        }
    }
    private void Start()
    {
        var position = FindObjectOfType<Grid>().WorldToCell(transform.position);
        SpreadingFire.FireTilemap.SetTile(position, tile);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
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