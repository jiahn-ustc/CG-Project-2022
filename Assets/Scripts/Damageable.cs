using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Health _health;
    public PlayerMovement PM;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void InflictDamage(float damage)
    {
        PM.addHealth(0.5f *damage);
        _health.TakeDamage(damage);
    }
}
