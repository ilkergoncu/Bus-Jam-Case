using System;
using UnityEngine;
//<summary
//This class holds the values for characters. It should be added to every stickman and bus object in the game.
//<summary
public class CharacterAi : Character
{
 public ColorType ColorType; 
 public ControllerCharacterVisual ControllerCharacterVisual; 
 public ManagerGrid.BusLineSlot currentSlot;
 public event Action OnMoveComplete;

       

    private void Awake()
    {

     ControllerCharacterVisual = GetComponent<ControllerCharacterVisual>();
       
    }


    public void MoveTo(GridPart targetGridPart)
    {
        transform.position = targetGridPart.transform.position; 
        OnMoveComplete?.Invoke(); 
    }

}


