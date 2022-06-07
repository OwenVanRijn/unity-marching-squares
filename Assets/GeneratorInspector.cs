using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generator))]
public class GeneratorInspector : Editor
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Generator generator = target as Generator;
        if (GUILayout.Button("Generate"))
        {
            generator.Generate();   
        }
    }
}
