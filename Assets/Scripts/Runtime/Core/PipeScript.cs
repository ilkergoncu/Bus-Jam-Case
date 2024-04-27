using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GridPart pipeGridPart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateCharacter();
        }
    }

    public void CreateCharacter()
    {

        if (pipeGridPart.InsideCharacterAi == null)
        pipeGridPart.CreateACharacter();
    }
}
