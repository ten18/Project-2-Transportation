using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public static float scaleX, scaleY, scaleZ;
    public int myX, myY;
    public static bool activePlane;

    // Start is called before the first frame update
    void Start()
    {
        scaleX = 0.5f;
        scaleY = 0.5f;
        scaleZ = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void OnMouseDown()
    {
        if (myX == Transportation.airplaneX && myY == Transportation.airplaneY) // if you click on the airplane...
        {
            if (activePlane)
            {
                activePlane = false;
                Transportation.grid[Transportation.airplaneX,Transportation.airplaneY].transform.localScale -= new Vector3(scaleX, scaleY, scaleZ);
            }
            else
            {
                activePlane = true;
                Transportation.grid[Transportation.airplaneX, Transportation.airplaneY].transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
            }
        }

        // Space-by-Space Code
        else // if you don't click on the airplane...
        {
            if (activePlane)
            {
                Transportation.destinationX = myX;
                Transportation.destinationY = myY;
                Transportation.destination = gameObject;
                //print("Destination is " + (Transportation.destinationX + 1) + ", " + (Transportation.destinationY + 1));
            }
        }

        /* Teleporter Code (keeping as comment for future reference)
       else // if you don't click on the airplane...
       {
           if (activePlane)
           {
               Transportation.selectedCube.GetComponent<Renderer>().material.color = Color.white;
               Transportation.depotCube.GetComponent<Renderer>().material.color = Color.black;
               Transportation.selectedCube.transform.localScale -= new Vector3(scaleX, scaleY, scaleZ);
               Transportation.airplaneX = myX;
               Transportation.airplaneY = myY;
               Transportation.selectedCube = gameObject;
               Transportation.selectedCube.GetComponent<Renderer>().material.color = Color.red;
               Transportation.selectedCube.transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
           }
       }
       */
    }
}
