using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 350);
    }


    public void KillOldBullet()
    {
        Destroy(gameObject, 2.0f);
    }


    void OnCollisionEnter(Collision collisionInfo)
    {
        Destroy(gameObject, 0.0f);
    }
}