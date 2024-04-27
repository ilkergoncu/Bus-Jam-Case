using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridCreator))]
public class GridCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GridCreator gridCreator = (GridCreator)target;
        if (GUILayout.Button("Create A Grid"))
        {
            gridCreator.CreateGrid();;
        }
        
        if (GUILayout.Button("Remove The Grid"))
        {
            gridCreator.RemoveGrid();
        }

        if (GUILayout.Button("Remove All Bus Line Slots"))
        {
            gridCreator.RemoveAllBusLineSlots();
        }

        if (GUILayout.Button("Create Bus Line Slots"))
        {
            gridCreator.CreateBusLineSlots();
        }

        if (GUILayout.Button("Create Random Characters"))
        {
            gridCreator.SetCharacterRandomized();
        }


    }
}
