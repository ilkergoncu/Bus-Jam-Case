using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//<summary>
//This class holds the data for the character renderers.
//<summary>

public class ControllerCharacterVisual : MonoBehaviour
{
   public CharacterAi CharacterAi { get; private set;}

   public SkinnedMeshRenderer SkinnedMeshRenderer;

    public MeshRenderer busMeshRenderer;


   private void Awake()
   {
      CharacterAi = GetComponent<CharacterAi>();
      SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
   }

    private void Update()
    {
        
    }

    public void SetCharacterMaterial(Material material)
   {
        if (this.gameObject.tag == "Bus")
        {
            busMeshRenderer.sharedMaterial = material;
        }
        else
        {
            SkinnedMeshRenderer.material = material;
        }


     
   }

}
