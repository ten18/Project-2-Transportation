using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transportation : MonoBehaviour
{
    public static GameObject cubeObj;
    public GameObject cubePrefab;
    public static int cubeXPos, cubeYPos;
    Vector3 cubePos;
    public static GameObject activeCube;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 16; i++) // value starts at i, loop stops when i is 15 (goes 16 times), and i is increased by 1 each time
        {
            cubePos = new Vector3(0+cubeXPos, 0, 0);
            cubeObj = Instantiate(cubePrefab, cubePos, Quaternion.identity);
            cubeXPos += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
