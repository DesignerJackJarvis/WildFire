using UnityEngine;

interface IDamageAble
{
    bool TakeDamage(float damage);
    Vector3 GetPos();
}