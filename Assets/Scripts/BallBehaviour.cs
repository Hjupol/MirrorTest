using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallBehaviour : NetworkBehaviour
{
    public Rigidbody rb;

    public void Awake()
    {
        rb.detectCollisions = false;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        rb.detectCollisions = true;
    }

    [ServerCallback]
    void OnCollisionEnter2D(Collision2D col)
    {
        //Do something...
    }
}
