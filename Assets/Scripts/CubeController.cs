using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
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
        if (Transportation.activeCube != null) // if none of the cubes have been interacted with yet, then don't turn the first cube white
        {
            Transportation.activeCube.GetComponent<Renderer>().material.color = Color.white; // turn the last clicked cube white otherwise
        }
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        Transportation.activeCube = gameObject;
    }
}
