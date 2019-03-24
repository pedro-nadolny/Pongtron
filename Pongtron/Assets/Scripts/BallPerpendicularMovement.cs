using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPerpendicularMovement : MonoBehaviour
{

    public Rigidbody rigidbody;
    public Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = speed;
    }
}
