using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletDamage;
    public Rigidbody rb;


    public void Initialise(float bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.GetDamage(bulletDamage);
            Destroy(gameObject, 0.01f);
        }
    }
}
