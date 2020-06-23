using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileController : NetworkBehaviour
{
    public Rigidbody rb;

    public override void OnStartServer()
    {
        base.OnStartServer();

        rb.detectCollisions = true;
    }

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(WaitDisapear(0));
        }
    }

    public void Disappear()
    {
        GameObject explosion;
        explosion = GameObject.Find("TestParticles2");

        GameObject particles = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

        Destroy(particles, 3);
        Destroy(gameObject);
    }

    public void ExcecuteDisapear(int time)
    {
        StartCoroutine(WaitDisapear(time));
    }
    public IEnumerator WaitDisapear(int time)
    {
        yield return new WaitForSeconds(time);
        Disappear();
    }
}
