using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportation : MonoBehaviour
{
    public static GameObject clickedCube; // the cube that is or could become the plane
    public static GameObject depotCube; // the delivery depot, stationary
    public GameObject cubePrefab;
    Vector3 cubePos;
    public static GameObject[,] grid;
    public int length, height; // dimensions of play area
    public static int airplaneX, airplaneY;
    public static int depotX, depotY;
    float goNow;
    public float turnLegnth; // how long each turn lasts, works in tandem with goNow
    public int airplaneCargo;
    public int score;
    public int cargoGain; //amount of cargo that is loaded each turn

    void LoadCargo()
    {
        if (airplaneCargo < 90)
        {
            airplaneCargo += cargoGain;
        }
    }

    void DeliverCargo()
    {
        score += airplaneCargo;
        airplaneCargo = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        turnLegnth = 1.5f;
        goNow = turnLegnth;
        length = 16;
        height = 9;
        grid = new GameObject[length, height];
        airplaneCargo = 0;
        score = 0;
        cargoGain = 10;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < length; x++) // value starts at x, loop stops when x is 15 (goes 16 times), and x is increased by 1 each time
            {
                //SpawnCube(y * -2, x * 2); insert method here
                cubePos = new Vector3(x*2, y*-2, 0); // Grid is created from left to right, top to bottom (like reading a book)
                grid[x,y] = Instantiate(cubePrefab, cubePos, Quaternion.identity);
                grid[x, y].GetComponent<CubeController>().myX = x;
                grid[x, y].GetComponent<CubeController>().myY = y;
            }
        }
        // airplane starts in upper left
        airplaneX = 0;
        airplaneY = 0;
        clickedCube = grid[airplaneX,airplaneY];
        clickedCube.GetComponent<Renderer>().material.color = Color.red;
        CubeController.activePlane = false;

        // delivery depot is in lower right
        depotX = 15;
        depotY = 8;
        depotCube = grid[depotX, depotY];
        depotCube.GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > goNow)
        {
            goNow += turnLegnth;
            
            // is airplane in starting position?
            if (airplaneX == 0 && airplaneY == 0)
            {
                LoadCargo();
            }

            // is airplane at the depot?
            if (airplaneX == depotX && airplaneY == depotY)
            {
                DeliverCargo();
            }

            print("Airplane cargo: " + airplaneCargo + "/90 tons");
            print("Score: " + score);
        }
    }
}
