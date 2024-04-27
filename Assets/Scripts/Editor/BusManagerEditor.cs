using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BusManager))]
public class BusManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BusManager busManager = (BusManager)target;
        if (GUILayout.Button("Create A Bus"))
        {
            busManager.CreateBus();
        }

        if (GUILayout.Button("Remove The Bus"))
        {
            busManager.RemoveBus();
        }

        

    }
}




