using UnityEngine;

public class StationaryTower : MonoBehaviour
{
    public float attackInterval;
    public float damage;
    private float _tick;
    public Vector3 direction;
    public float range;
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
}
