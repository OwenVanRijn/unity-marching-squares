
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private Point[][] points;
    public int size;
    public float scale;
    public GameObject prefab;
    public Vector3 moveX;
    public Vector3 moveY;
    public float isoValue;
    public Material red;
    public Material green;
    public Transform parent;
    public Vector2 offset;
    public bool perlinNoise;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
         while (parent.childCount > 0)
            DestroyImmediate(parent.GetChild(0).gameObject);
        
        points = new Point[size][];
        for (int x = 0; x < size; x++)
        {
            Point[] current = new Point[size];
            for (int y = 0; y < size; y++)
            {
                GameObject newObject = Instantiate(prefab, moveX * x + moveY * y, Quaternion.identity);
                newObject.transform.parent = parent;
                newObject.name = $"{x}:{y}";
                current[y] = newObject.GetComponent<Point>();
                current[y].generator = this;
                float a = x * scale;
                float b = y * scale;
                Debug.Log($"{a} {b}");
                current[y].height = (perlinNoise) ? Mathf.PerlinNoise(a + offset.x, b + offset.y) : Random.value;
                Debug.Log(current[y].height);
            }

            points[x] = current;
        }

        for (int x = 0; x < (size - 1); x++)
        {
            for (int y = 0; y < (size - 1); y++)
            {
                Point bottomLeft = points[x][y];
                Point bottomRight = points[x][y + 1];
                Point topLeft = points[x + 1][y];
                Point topRight = points[x + 1][y + 1];

                int combined = (bottomLeft.IsOne ? 0x1 : 0) | (bottomRight.IsOne ? 0x2 : 0) | (topRight.IsOne ? 0x4 : 0) |
                                (topLeft.IsOne ? 0x8 : 0);

                Debug.Log(combined);
                
                switch (combined)
                {
                    // Bottom left ON
                    case 0x1:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Bottom right ON
                    case 0x2:
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Bottom left ON
                    // Bottom right ON
                    case 0x3:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top right ON
                    case 0x4:
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top right ON
                    // Bottom left ON
                    case 0x5:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top right ON
                    // Bottom right ON
                    case 0x6:
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top right ON
                    // Bottom right ON
                    // Bottom left ON
                    case 0x7:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    case 0x8:
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Bottom left ON
                    case 0x9:
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Bottom right ON
                    case 0xA:
                        //bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        //topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Bottom right ON
                    // Bottom left ON
                    case 0xB:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        //topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Top right ON
                    case 0xC:
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Top right ON
                    // Bottom left ON
                    case 0xD:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        //bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // Top left ON
                    // Top right ON
                    // Bottom right ON
                    case 0xE:
                        //bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                    // ALL
                    case 0xF:
                        bottomLeft.topRightClose.GetComponent<Renderer>().enabled = false;
                        bottomLeft.topRightFar.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftClose.GetComponent<Renderer>().enabled = false;
                        topRight.bottomLeftFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightFar.GetComponent<Renderer>().enabled = false;
                        topLeft.bottomRightClose.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftFar.GetComponent<Renderer>().enabled = false;
                        bottomRight.topLeftClose.GetComponent<Renderer>().enabled = false;
                        break;
                }
            }
        }
    }
}
