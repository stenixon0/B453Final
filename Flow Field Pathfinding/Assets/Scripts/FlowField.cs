using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    /* 
     * Copied from https://github.com/nature-of-code/noc-examples-processing/tree/master/chp06_agents/NOC_6_04_Flowfield
     * 
     * Any comments from me will be bracketed, otherwise they are copied from the original
     * 
     * A flow field is a two dimensional array of PVectors
     * 
     * [TODO] adapt main block of code
     * 
     */


    /*
     * Variables below are from an adaptation of this tutorial by Peter Olthof of Peer Play
     * https://www.youtube.com/watch?v=gPNdnIMbe8o
     * 
     */
    public Vector3[,,] flowfieldDirection;
    public float cellSize = 2;
    public Vector3Int gridSize = new Vector3Int(10, 10, 10);
    public float increment = 0.5f;
    //public Vector3 _offset, offsetSpeed;

    public bool debug = true;

    //Adapted from Peter Olthof
    public FlowField()
    {
        flowfieldDirection = new Vector3[gridSize.x, gridSize.y, gridSize.z];

    }
    private void Start()
    {
        CalculateFlowFieldDirections();
    }

    //TODO: Lookup
    public Vector3 lookup(Vector3 lookup)
    {
        return flowfieldDirection[
            Mathf.RoundToInt(lookup.x / cellSize),
            Mathf.RoundToInt(lookup.y / cellSize),
            Mathf.RoundToInt(lookup.z / cellSize)];
    }

    /* 
     * Below code adapted from https://www.youtube.com/watch?v=gPNdnIMbe8o
     * 
     */
    void CalculateFlowFieldDirections()
    {
        float xOff = 0f;

        for (int x = 0; x < gridSize.x; x++)
        {
            float yOff = 0;
            for (int y = 0; y < gridSize.y; y++)
            {
                float zOff = 0f;
                for (int z = 0; z < gridSize.z; z++)
                {
                    //float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = Vector3.up; //new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    Vector3 nd = noiseDirection.normalized;

                    flowfieldDirection[x, y, z] = nd;

                    zOff += increment;
                }
                yOff += increment;
            }
            xOff += increment;
        }
    }
    private void OnDrawGizmos()
    {
        if (debug)
        {
            for (int x = 0; x < gridSize.x; x++)
            {

                for (int y = 0; y < gridSize.x; y++)
                {

                    for (int z = 0; z < gridSize.x; z++)
                    {
                        Vector3 nd = flowfieldDirection[x, y, z];
                        Gizmos.color = new Color(nd.x, nd.y, nd.z, 0.4f);
                        Vector3 pos = new Vector3(x, y, z);// + transform.position;
                        Vector3 endpos = pos + Vector3.Normalize(Vector3.zero);
                        Gizmos.DrawLine(pos, endpos);
                        Gizmos.DrawSphere(endpos, 0.1f);
                    }
                }
            }
        }
    }
}
