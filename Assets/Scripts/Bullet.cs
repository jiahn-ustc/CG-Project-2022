using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        OnHitWall(other);
    }

    IEnumerator OnHitWall(Collider other)
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}