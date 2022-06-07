using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Generator generator;
    public float height;
    public Transform bottomLeftClose, bottomLeftFar, bottomRightClose, bottomRightFar, topLeftClose, topLeftFar, topRightClose, topRightFar;
    public Renderer testCube;

    public bool IsOne => IsOneFunc();

    private bool IsOneFunc()
    {
        bool val = generator.isoValue > height;
        testCube.material = val ? generator.green : generator.red;
        return val;
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
