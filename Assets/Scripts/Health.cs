using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public bool isDead { get; private set; }
    //public PlayerMovement PM;

    private float _currentHealth;

    private MeshRenderer[] _meshes;
    private Animator _animator;
    private void Awake()
    {
        _meshes = GetComponentsInChildren<MeshRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(value: _currentHealth, min: 0, maxHealth);

        Debug.Log(message: "current health: " + _currentHealth);

        StartCoroutine(routine: OnDamage());
    }

    IEnumerator OnDamage()
    {
        foreach(MeshRenderer mesh in _meshes)
        {
            mesh.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.01f);

        if(_currentHealth > 0)
        {
            foreach(MeshRenderer mesh in _meshes)
            {
                mesh.material.color = Color.white;
            }
        }
        else if(!isDead)
        {
            foreach (MeshRenderer mesh in _meshes)
            {
                mesh.material.color = Color.gray;
            }

            isDead = true;
            // PM.addHealth(80);
            _animator.SetTrigger(name:"doDie");
            Destroy(gameObject, t:3);
        }
    }
}
