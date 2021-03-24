using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://github.com/nature-of-code/noc-examples-processing/tree/master/chp06_agents/NOC_6_01_Seek 
 * TODO: Credits for code and concepts
 */

public class AutoBoat : MonoBehaviour
{
    Rigidbody rb;
    public GameObject targetGO;
    Vector3 targetV;

    /*Adapted from Vehicle.pde lines 11-16*/
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    float r;
    float maxforce;
    float maxspeed;

    /*Adapted from Vehicle.pde*/

    AutoBoat(float x, float y)
    {
        rb = GetComponent<Rigidbody>();
        acceleration = Vector3.zero;
        velocity = Vector3.forward;
        position = new Vector3(x, y);
        r = 6f;
        maxspeed = 4f;
        maxforce = 0.1f;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetV = targetGO.transform.position;
    }

    private void UpdatePosition()
    {
        velocity += acceleration;
        position += velocity;
        acceleration = Vector3.zero;
    }

    void applyForce(Vector3 force)
    {
        acceleration += force;
    }

    void seek(Vector3 target)
    {
        Vector3 desired = target - position;
        desired = Vector3.ClampMagnitude(desired, maxspeed);
        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxforce);
        applyForce(steer);
    }

}
