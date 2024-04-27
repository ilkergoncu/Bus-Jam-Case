using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//<summary>
//This class holds the data of busses in order the check thet are full. Also it handles the animation of the buses with dotween.
//<summary>


public class Bus : MonoBehaviour
{
   

    [SerializeField]
    private int passengers = 0;
    public int capacity = 3;
    public Transform[] seats;
    public bool IsFull => passengers >= capacity;

    
    public float moveYAmount = 2f;  
    public float duration = 1f;     
  

   void Start()
   {
        //Starts the DOTween animation.
     transform.DOMoveY(transform.position.y + moveYAmount, duration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo); 
   }
    
    
    //Moves on to the next scene.
    public Transform GetNextAvailableSeat()
    {
        if (passengers < capacity)
        {
            return
                seats[passengers++];
           
           
        }
        return null;
    }


}