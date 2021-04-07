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
    Vector2[,] field;
    int cols, rows; // Columns and Rows
    int resolution; // How large is each "cell" of the flow field

    FlowField(int r)
    {
        resolution = r;
        cols = resolution; // [??? Original code based on width of sketch]
        rows = resolution; // [??? Same]
        field = new Vector2[cols, rows];
        init();
    }

    void init()
    {
        //TODO: Fast Noise applied in context noiseSeed((int)random(10000));
        float xoff = 0;
        for (int i = 0; i < cols; i++)
        {
            float yoff = 0;
            for (int j = 0; j < rows; j++)
            {
                float theta = 0; //[map(noise(xoff,yoff), 0,1,0,TWO_PI); <- adapt for fast noise]
                // Polar to cartesian coordinate transformation to get x and y [and Z] components of the vector
                yoff += 0.1f;
            }
            xoff += 0.1f;
        }
    }

    void display()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                //[TODO: Adapt cone primitives to appropriately display vector length and direction] 
                //drawVector(field[i][j], i * resolution, j*resolution, resolution-2);
                break;
            }
        }
    }
    //TODO: DrawVector

    void drawVector(Vector2 v, float x, float y, float scayl)
    {
        
    }

    //TODO: Lookup
    public Vector3 lookup(Vector3 lookup)
    {
        return lookup;
    }
}
