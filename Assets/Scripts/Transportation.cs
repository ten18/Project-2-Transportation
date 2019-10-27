using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportation : MonoBehaviour
{
    public static GameObject clickedCube;
    public GameObject cubePrefab;
    int row;
    Vector3 cubePos;
    public static GameObject[,] grid;
    public int length, height;
    public static int airplaneX, airplaneY;

    void SpawnCube(int yPos, int xPos) // Empty method for spawning the cubes.
    {                                  // No longer in use because storing cubes in the array requires returning variables and I don't know how to do that yet.
    }

    // Start is called before the first frame update
    void Start()
    {
        length = 16;
        height = 9;
        row = 0;
        grid = new GameObject[length, height];
        for (int y = 0; y < height; y++)
        {
            row += 1;
            for (int x = 0; x < length; x++) // value starts at x, loop stops when x is 15 (goes 16 times), and x is increased by 1 each time
            {
                //SpawnCube(y * -2, x * 2);
                cubePos = new Vector3(x*2, y*-2, 0);
                grid[x,y] = Instantiate(cubePrefab, cubePos, Quaternion.identity);
                grid[x, y].GetComponent<CubeController>().myX = x;
                grid[x, y].GetComponent<CubeController>().myY = y;
            }
        }
        // airplane is in upper left
        airplaneX = 0;
        airplaneY = 0;
        clickedCube = grid[airplaneX,airplaneY];
        clickedCube.GetComponent<Renderer>().material.color = Color.red;
        CubeController.activePlane = false;
    }

    // Update is called once per frame
    void Update()
    {
        clickedCube.GetComponent<Renderer>().material.color = Color.red;
    }
}
