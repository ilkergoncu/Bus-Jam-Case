using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//<summary>
//This class holds values for each grid parts.
//It also has functions the create and remove characters in each grid for level editing purposes.
//<summary>

public class GridPart : MonoBehaviour
{

    public int NoRow, NoColumn;

    public float FromStartCost;
    public float ToEndCost;
    public float TotalCost;

    public GridPart PreviousGirdPart;

    [SerializeField]
    private CharacterAi _insideCharacterAi;
    public CharacterAi InsideCharacterAi => _insideCharacterAi;

    [SerializeField] private CharacterAi _prefabCharacterAi;
    public ColorType ColorType; 
    public ColorDataSO ColorDataSo;


 
    private void Start()
    {
        CharacterAi characterAi = GetComponentInChildren<CharacterAi>();
        if (characterAi)
        {
            _insideCharacterAi = characterAi;
        }
    }

    public void SetInsideCharacterAi(CharacterAi characterAi)
    {
        _insideCharacterAi = characterAi;
    }

    public void SetTotalCost()
    {
        TotalCost = FromStartCost + ToEndCost; // Gets to total cost for the A* pathfinding.
    }

    //<summary>
    //This function creates a character in the selected grid. It gets called int the GridCreator class for random character creating.
    //<summary>


    public void CreateACharacter()
    {
        if(_insideCharacterAi != null) return;
        
        CharacterAi characterAi = Instantiate(_prefabCharacterAi);
        characterAi.transform.position = transform.position;
        characterAi.transform.SetParent(transform);
        _insideCharacterAi = characterAi;

        characterAi.ColorType = ColorType;
        characterAi.ControllerCharacterVisual.SetCharacterMaterial(GetMaterialAccordingToColorType(ColorType));
    }


    public void RemoveTheCharacter() //Removes the character in the selected grid.
    {
        if(_insideCharacterAi == null) return;
        
        DestroyImmediate(_insideCharacterAi.gameObject);
    }
    
    
    private Material GetMaterialAccordingToColorType(ColorType colorType)
    {
        return ColorDataSo.ColorDatas.First(x => x.ColorType == colorType).Material;
    }


}
