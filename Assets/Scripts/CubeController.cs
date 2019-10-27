using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    float x, y, z;
    public int myX, myY;
    public static bool activePlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        x = 0.5f;
        y = 0.5f;
        z = 0.5f;
        if (myX == Transportation.airplaneX && myY == Transportation.airplaneY) // if you click on the airplane...
        {
            if (activePlane)
            {
                activePlane = false;
                Transportation.clickedCube.transform.localScale -= new Vector3(x, y, z);
            }
            else
            {
                activePlane = true;
                Transportation.clickedCube.transform.localScale += new Vector3(x, y, z);
            }
        }
        else // if you don't click on the airplane...
        {
            if (activePlane)
            {
                Transportation.clickedCube.GetComponent<Renderer>().material.color = Color.white;
                Transportation.clickedCube.transform.localScale -= new Vector3(x, y, z);
                Transportation.airplaneX = myX;
                Transportation.airplaneY = myY;
                Transportation.clickedCube = Transportation.grid[Transportation.airplaneX, Transportation.airplaneY];
                Transportation.clickedCube.transform.localScale += new Vector3(x, y, z);
            }
        }
        /*
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.localScale += new Vector3(x, y, z);
        Transportation.activePlane = gameObject;
        */
    }
}
