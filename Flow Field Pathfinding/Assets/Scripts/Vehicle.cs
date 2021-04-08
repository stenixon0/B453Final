using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    /*
     * Code adapted from https://github.com/nature-of-code/noc-examples-processing/blob/master/chp06_agents/NOC_6_04_Flowfield/Vehicle.pde
     * 
     * The Nature of Code
     * Daniel Shiffman
     * http://natureofcode.com
     * 
     */

    Vector3 velocity;
    Vector3 acceleration;

    //[my addition to expose width and height to the editor]
    public float height = 20f, width = 20f;

    float r;
    float maxforce; // Maximum steering force
    float maxspeed; // Maximum speed
    private void Start()
    {
        r = 3.0f;
        maxspeed = 3f;
        maxforce = 0.2f;
    }
    public void constructVehicle(Vector3 l, float ms, float mf)
    {
        transform.position = l;
        r = 3.0f;
        maxspeed = ms;
        maxforce = mf;
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
    }

    public void run()
    {
        //TODO: update borders display
    }

    // Implementing Reynolds' flow field following algorithm
    // http://www.red3d.com/cwr/steer/FlowFollow.html
    public void follow(FlowField flow)
    {
        // What is the vector at that spot in the flow field?
        Vector3 desired = flow.lookup(transform.position);
        // Scale it up by maxpeed
        desired = desired * maxspeed;
        // Steering is desired - velocity
        Vector3 steer = desired - velocity;
        //steer.limit(maxforce) <- TODO find Unity's limit equivalent
        applyForce(steer);
    }

    void applyForce(Vector3 force)
    {
        //We could add mass here if we want A = F / M
        acceleration += force;
    }

    // Method to update position
    private void Update()
    {
        //Update velocity
        velocity += acceleration;
        // Limit Speed
        //velocity.limit(maxspeed); <- TODO: find Unity's limit equivalent
        transform.position += velocity;
        acceleration *= 0;
    }

    //TODO: Adapt display()
    
    void borders()
    {
        //[below line added to better fit adapted code
        Vector3 position = transform.position;

        if (position.x < -r) position.x = width + r;
        if (position.y < -r) position.y = height + r;
        if (position.x > width + r) position.x = -r;
        if (position.y > height + r) position.y = -r;
    }
    

}
