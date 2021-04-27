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
    public FlowField flowfield;

    Vector3 velocity;
    Vector3 acceleration;

    //[my addition to expose width and height to the editor]
    //public float height = 20f, width = 20f;

    //float r;
    float maxforce; // Maximum steering force
    float maxspeed; // Maximum speed

    bool in_bounds = true;


    //New Additions
    float current_temp;
    public float initial_temp = 100f;
    float lcl;
    public float dewpoint = 40f;
    public void constructVehicle(float ms, float mf, FlowField flow)
    {
        //r = 3.0f;
        maxspeed = ms;
        maxforce = mf;
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        flowfield = flow;
        current_temp = initial_temp;
        lcl = (initial_temp - dewpoint) / 8.3f;
    }

    public void movementManager()
    {
        /*if (in_bounds)
        {
            Vector3Int index = boundsCheck();
            Vector3Int failExample = new Vector3Int(-1, -1, -1);
            if (index == failExample) in_bounds = false;
            else applyForce(flowfield.lookup(index));
        } */
        /*else
        {
            //Vehicle continues with set velocity
            applyForce(Vector3.zero);
        }*/
        updateTemp();
        applyBuoyancy();
    }

    // Implementing Reynolds' flow field following algorithm
    // http://www.red3d.com/cwr/steer/FlowFollow.html
    //Has become inert
    public void follow(Vector3Int index)
    {
        // What is the vector at that spot in the flow field?
        //High risk of OOB Exception -> movementManager should handle this
        Vector3 desired = flowfield.lookup(index);
        // Scale it up by maxpeed
        desired = desired * maxspeed;
        // Steering is desired - velocity
        Vector3 steer = desired - velocity;
        Vector3.ClampMagnitude(steer, maxforce);

        applyForce(steer);
    }
    
    Vector3Int boundsCheck()
    {
        Vector3Int gridSize = flowfield.getGridSize();
        float cellSize = flowfield.getCellSize();
        Vector3 tp = transform.position;

        //[from a transform.position, the corresponding index is returned]
        Vector3 locationConversion =
            new Vector3(
                tp.x / cellSize + gridSize.x / 2,
                tp.y / cellSize + gridSize.y / 2,
                tp.z / cellSize + gridSize.z / 2);
        //[simplifies bound corrections]
        Vector3 gridConversion =
            new Vector3(
                gridSize.x / 2 * cellSize,
                gridSize.y / 2 * cellSize,
                gridSize.z / 2 * cellSize
                );


        //Mutates transform.position to fit within bounds
        Vector3Int failedCheck = new Vector3Int(-1, -1, -1);
        if (locationConversion.x >= gridSize.x - 1) return failedCheck;
        if (locationConversion.x < 0) return failedCheck;

        if (locationConversion.y >= gridSize.y -  1) return failedCheck;
        if (locationConversion.y < 0) return failedCheck;

        if (locationConversion.z >= gridSize.z - 1) return failedCheck;
        if (locationConversion.z < 0) return failedCheck;

        //New transform.position converted to appropriate index (should fit within bounds now)
        int x = Mathf.FloorToInt(tp.x / cellSize + gridSize.x / 2);
        int y = Mathf.FloorToInt(tp.y / cellSize + gridSize.y / 2);
        int z = Mathf.FloorToInt(tp.z / cellSize + gridSize.z / 2);
        return new Vector3Int(x, y, z);
    }


    void applyForce(Vector3 force)
    {
        //These functions are convoluted, how will acceleration ever increase?
        acceleration += force;
        //borders();
        //Update velocity
        velocity += acceleration;
        // Limit Speed
        velocity = Vector3.ClampMagnitude(velocity, maxspeed);
        transform.position += velocity;
        acceleration *= 0;
    }


    //TODO: Update Temp, Check buoyancy, check env temp
    void updateTemp()
    {
        float temp_change = -6.5f * transform.position.y; /*= (transform.position.y < lcl) ? 
            -10f * transform.position.y:
            -10f * lcl - 6.5f * (transform.position.y - lcl);*/
        current_temp = initial_temp + temp_change;
    }
    float check_environment_temp()
    {
        if (transform.position.y < 15f) //tropopause at 15km
        {
            return initial_temp + transform.position.y * -8f; //-8 C/km = Environment cooling (lapse) rate 
        } 
        else
        {
            return -1f; 
        }
    }
    void applyBuoyancy()
    {
        float etemp = check_environment_temp();
        if (etemp == -1f) applyForce(Vector3.down);
        else applyForce((etemp < current_temp) ? Vector3.up: Vector3.down);
    }
}
