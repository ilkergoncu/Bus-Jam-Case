using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridPart))]
public class GridPartEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridPart gridPart = (GridPart)target;
        if (GUILayout.Button("Create A Character"))
        {
            gridPart.CreateACharacter();
        }
        

        if (GUILayout.Button("Remove The Character"))
        {
            gridPart.RemoveTheCharacter();
        }

    }
}
