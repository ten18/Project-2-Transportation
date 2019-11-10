using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transportation : MonoBehaviour
{
    //public static GameObject selectedCube;  irrelevant to the code at this point, keeping for reference
    public static GameObject depotCube; // the delivery depot, stationary
    public static GameObject destination;
    public GameObject cubePrefab;
    public Text infoText;
    Vector3 cubePos;
    public static GameObject[,] grid;
    public int length, height; // dimensions of play area
    public static int airplaneX, airplaneY;
    public static int depotX, depotY;
    public static int destinationX, destinationY;
    public static float goNow;
    public static float turnLength; // how long each turn lasts, works in tandem with goNow
    public int airplaneCargo;
    public int score;
    public int cargoGain; //amount of cargo that is loaded each turn
    int moveX, moveY;

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

    void PathFind() // find best direction for airplane to move next
    {
        {
            // NORTH
            if (destinationX == airplaneX && destinationY < airplaneY)
            {
                moveX = 0;
                moveY = -1;
            }

            // SOUTH
            if (destinationX == airplaneX && destinationY > airplaneY)
            {
                moveX = 0;
                moveY = 1;
            }

            // EAST
            if (destinationX > airplaneX && destinationY == airplaneY)
            {
                moveX = 1;
                moveY = 0;
            }

            // WEST
            if (destinationX < airplaneX && destinationY == airplaneY)
            {
                moveX = -1;
                moveY = 0;
            }

            // SOUTHEAST
            if (destinationX > airplaneX && destinationY > airplaneY)
            {
                if ((destinationX - airplaneX) == (destinationY - airplaneY))
                {
                    moveX = 1;
                    moveY = 1;
                }
                if ((destinationX - airplaneX) > (destinationY - airplaneY))
                {
                    moveX = 1;
                    moveY = 0;
                }
                if ((destinationX - airplaneX) < (destinationY - airplaneY))
                {
                    moveX = 0;
                    moveY = 1; // positive for down (and negative for up) because coord system starts in upper left
                }
            }

            // NORTHEAST
            if (destinationX > airplaneX && destinationY < airplaneY)
            {
                if ((destinationX - airplaneX) == (airplaneY - destinationY))
                {
                    moveX = 1;
                    moveY = -1;
                }
                if ((destinationX - airplaneX) > (airplaneY - destinationY))
                {
                    moveX = 1;
                    moveY = 0;
                }
                if ((destinationX - airplaneX) < (airplaneY - destinationY))
                {
                    moveX = 0;
                    moveY = -1;
                }
            }

            // NORTHWEST
            if (destinationX < airplaneX && destinationY < airplaneY)
            {
                if ((airplaneX - destinationX) == (airplaneY - destinationY))
                {
                    moveX = -1;
                    moveY = -1;
                }
                if ((airplaneX - destinationX) > (airplaneY - destinationY))
                {
                    moveX = -1;
                    moveY = 0;
                }
                if ((airplaneY - destinationY) > (airplaneX - destinationX))
                {
                    moveX = 0;
                    moveY = -1;
                }
            }

            // SOUTHWEST
            if (destinationX < airplaneX && destinationY > airplaneY)
            {
                if ((airplaneX - destinationX) == (destinationY - airplaneY))
                {
                    moveX = -1;
                    moveY = 1;
                }
                if ((airplaneX - destinationX) > (destinationY - airplaneY))
                {
                    moveX = -1;
                    moveY = 0;
                }
                if ((airplaneX - destinationX) < (destinationY - airplaneY))
                {
                    moveX = 0;
                    moveY = 1;
                }
            }

            // STOP
            if (destinationX == airplaneX && destinationY == airplaneY)
            {
                moveX = 0;
                moveY = 0;
            }
        }
    }

    void MoveAirplane()
    {
        if (CubeController.activePlane)
        {
            if (airplaneX == depotX && airplaneY == depotY)
            {
                depotCube.GetComponent<Renderer>().material.color = Color.black;
            }
            else
            {
                grid[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.white;
            }

            grid[airplaneX,airplaneY].transform.localScale /= 1.5f;


            // move plane along x axis and define x boundaries
            if (moveX == 1 && airplaneX < length-1)
            {
                airplaneX += moveX;
            }
            if (moveX == -1 && airplaneX > 0)
            {
                airplaneX += moveX;
            }

            // move plane along y axis and define y boundaries
            if (moveY == 1 && airplaneY < height - 1)
            {
                airplaneY += moveY;
            }
            if(moveY == -1 && airplaneY > 0)
            {
                airplaneY += moveY;
            }

            grid[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.red;
            grid[airplaneX,airplaneY].transform.localScale *= 1.5f;
        }

        // movement will not be continuous, so reset and wait for next input
        //moveX = 0;
        //moveY = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "Cargo: " + airplaneCargo + "/90 " + "Score: " + score;
        turnLength = 1.5f;
        goNow = turnLength;
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
                cubePos = new Vector3(x*1.7f, y*-1.7f, 0); // Grid is created from left to right, top to bottom (like reading a book)
                grid[x,y] = Instantiate(cubePrefab, cubePos, Quaternion.identity);
                grid[x, y].GetComponent<CubeController>().myX = x;
                grid[x, y].GetComponent<CubeController>().myY = y;
            }
        }
        // airplane starts in upper left
        airplaneX = 0;
        airplaneY = 0;
        grid[airplaneX,airplaneY].GetComponent<Renderer>().material.color = Color.red;
        CubeController.activePlane = false;

        // delivery depot is in lower right
        depotX = 15;
        depotY = 8;
        depotCube = grid[depotX, depotY];
        depotCube.GetComponent<Renderer>().material.color = Color.black;

        // destination is not set yet, but ready to be set
        destination = grid[destinationX, destinationY];

        // airplane is not moving yet
        moveX = 0;
        moveY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PathFind();
        
        if (Time.time > goNow)
        {
            MoveAirplane();

            goNow += turnLength;  

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

            infoText.text = "Cargo: " + airplaneCargo + "/90 " + "Score: " + score;
        }
    }
}
