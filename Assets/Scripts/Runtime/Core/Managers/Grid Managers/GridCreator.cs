using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

//<summary>
//This class is for creating the grids in the game.
//<summary>


[ExecuteInEditMode]
public class GridCreator : MonoBehaviour
{
    public Transform StartTransform;
    public Transform TargetTransform;
    public GridPart PrefabGridPart;

    public GameObject busLineSlot;


    public int RowCount;
    public int ColumnCount;

    public float DistanceBetweenGridParts;
    
    public GridPart[,] GridParts;

    public GameObject busLineSlotPrefab;

    public int numberOfBusLineSlots = 5;

    public float spacing = 2.0f;

    public GameObject busLineSlotsPoint;

    public List<GameObject> slots = new List<GameObject>();

    //<summary>
    //This function creates the grid for the given row and column value.
    //<summary>

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        GridParts = new GridPart[RowCount,ColumnCount];
        
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                var newGridPart = Instantiate(PrefabGridPart);
                newGridPart.transform.position = StartTransform.position;
                newGridPart.transform.position += (DistanceBetweenGridParts * j * Vector3.right) - (DistanceBetweenGridParts * i * Vector3.forward);
                newGridPart.transform.SetParent(StartTransform);

                newGridPart.NoRow = i;
                newGridPart.NoColumn = j;

                GridParts[i, j] = newGridPart;
            }
        }

       
    }

    [ContextMenu("Create Bus Line Slots")]
    public void CreateBusLineSlots()
    {
        GridParts = new GridPart[RowCount, ColumnCount];

        for (int i = 0; i < numberOfBusLineSlots; i++)
        {
           GameObject slot = Instantiate(busLineSlotPrefab, busLineSlotsPoint.transform);
            slots.Add(slot);
            slot.gameObject.transform.localPosition = new Vector3(i * spacing, 0, 0);
           
        }


    }
    [ContextMenu("Remove All Bus Line Slots")]
    public void RemoveAllBusLineSlots()
    {
        foreach(GameObject slot in slots)
        {
            if(slot != null)
            {
                DestroyImmediate(slot);
            }
        }

        slots.Clear();
    }

    //<summary>
    //This function removes the last created grid.
    //<summary>

    [ContextMenu("Remove Grid")]
    public void RemoveGrid()
    {
        if(StartTransform.childCount == 0) return;

        for (int i = StartTransform.childCount-1; i >= 0 ; i--)
        {
            DestroyImmediate(StartTransform.GetChild(i).gameObject);
        }

        for (int i = TargetTransform.childCount-1; i >= 0 ; i--)
        {
            DestroyImmediate(TargetTransform.GetChild(i).gameObject);
        }
    }

    //<summary>
    //This function creates characters for given count and color in random grid parts.
    //<summary>

    public int RandomCharacterCount;
    public ColorType RandomCharacterColorType;
    [ContextMenu("Random Place")]
    public void SetCharacterRandomized()
    {
        var gridParts = FindObjectsOfType<GridPart>();
        for (int i = 0; i < RandomCharacterCount; i++)
        {
            var emptyGridParts = gridParts.Where(x => x.InsideCharacterAi == null).ToList();
            var emptyGridPart = emptyGridParts[Random.Range(0, emptyGridParts.Count - 1)];

            emptyGridPart.ColorType = RandomCharacterColorType;
            emptyGridPart.CreateACharacter();
        }
    }
}
