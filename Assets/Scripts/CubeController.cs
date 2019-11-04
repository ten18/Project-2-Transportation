using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    float scaleX, scaleY, scaleZ;
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
                Transportation.clickedCube.transform.localScale -= new Vector3(scaleX, scaleY, scaleZ);
            }
            else
            {
                activePlane = true;
                Transportation.clickedCube.transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
            }
        }
        else // if you don't click on the airplane...
        {
            if (activePlane)
            {
                Transportation.clickedCube.GetComponent<Renderer>().material.color = Color.white;
                Transportation.depotCube.GetComponent<Renderer>().material.color = Color.black;
                Transportation.clickedCube.transform.localScale -= new Vector3(scaleX, scaleY, scaleZ);
                Transportation.airplaneX = myX;
                Transportation.airplaneY = myY;
                Transportation.clickedCube = gameObject;
                Transportation.clickedCube.GetComponent<Renderer>().material.color = Color.red;
                Transportation.clickedCube.transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
            }
        }
    }
}
