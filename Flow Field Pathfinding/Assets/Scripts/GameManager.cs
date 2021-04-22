using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* 
     * The Nature of Code
     * Daniel Shiffman
     * http://natureofcode.com
     * 
     * Flow Field Following
     * Via Reynolds: http://www.red3d.com/cwr/steer/FlowFollow.html
     */


    // Flowfield object
    public FlowField flowfield;
    // An ArrayList of vehicles
    List<Vehicle> vehicles;
    
    public int vehicleCount; 

    //[My own adaptation of particle prefab from Peer Play]
    public Vehicle vehiclePrefab;

    private void Start()
    {
        flowfield = Instantiate(flowfield, Vector3.zero, Quaternion.identity);
        vehicles = new List<Vehicle>();

        //shorten variable name
        Vector3 tp = transform.position;
        for (int i =0; i < vehicleCount; i++)
        {
            //Copied from Peer Play and adjusted for negative offset (should really be calculated in flowfield)
            Vector3 randomPos = new Vector3(
                Random.Range(flowfield.cellSize * -flowfield.gridSize.x / 2, (flowfield.gridSize.x - 1) / 2 * flowfield.cellSize),
                Random.Range(flowfield.cellSize * -flowfield.gridSize.y / 2, (flowfield.gridSize.y - 1)/ 2 * flowfield.cellSize),
                Random.Range(flowfield.cellSize * -flowfield.gridSize.z / 2, (flowfield.gridSize.z - 1) / 2 * flowfield.cellSize)
                );
            Vehicle newVehicle = Instantiate(vehiclePrefab, randomPos, Quaternion.identity);
            newVehicle.constructVehicle(0.1f, 0.2f, flowfield); //[Placeholder values for initial position, mf, and ms]
            vehicles.Add(newVehicle);
        }
    }


    private void Update()
    {
        // Tell all the vehicles to follow the flow field
        foreach (Vehicle v in vehicles)
        {
            v.movementManager();
        }
    }

}
