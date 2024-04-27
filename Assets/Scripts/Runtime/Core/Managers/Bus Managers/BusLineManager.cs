using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.TextCore.Text;

//<summary>
//This class controls the characters waiting int the bus line to see if they should board the bus.
//<summary>

public class BusLineManager : MonoBehaviour
{
    public BusManager busManager; 
    public List<GameObject> waitingCharacters; 

    void Update()
    {
        CheckForBoarding();
    }

    //<summary>
    //This function handles the waiting characters and send them for boarding current the bus.
    //<summary>

    void CheckForBoarding()
    {
        if (busManager.currentBus != null && busManager.iscurrentBusReachedDestination)
        {
            GameObject currentBus = busManager.currentBus; //Gets the current bus.
            Bus busComponent = currentBus.GetComponent<Bus>();

            for (int i = waitingCharacters.Count - 1; i >= 0; i--)
            {
                GameObject character = waitingCharacters[i];
                CharacterAi characterComponent = character.GetComponent<CharacterAi>();

                // Checks the bus is the same color with the characters and if it is not full.
                if (characterComponent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color == currentBus.GetComponent<Renderer>().material.color && !busComponent.IsFull)
                {
                  
                    waitingCharacters.RemoveAt(i); 
                    character.GetComponent<Animator>().SetBool("isRunning", true);
                    Vector3 chScale = character.transform.localScale;
                    character.transform.DOMove(new Vector3(currentBus.transform.position.x,character.transform.position.y,currentBus.transform.position.z), 1).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        Debug.Log("Bus reached the destination.");
                        Transform seat = busComponent.GetNextAvailableSeat();
                        character.transform.localScale = Vector3.zero;

                        if (busManager.currentBus.GetComponent<Bus>().IsFull)
                        {
                            busManager.CheckBusCapacityAndManageNextBus(busManager.currentBus);
                        }

                        //Sits the character to the correct seat.
                        if (seat != null)
                        {
                           
                            character.transform.SetParent(seat);
                            character.transform.DOScale(new Vector3(15,15,15), 0.5f);
                            character.GetComponent<Animator>().SetTrigger("isSitting");
                            character.GetComponent<CharacterAi>().currentSlot.isOccupied = false;
                            character.GetComponent<CharacterAi>().currentSlot = null;
                            character.transform.position = seat.position; 
                            
                           
                        }
                    });

                   
                }
            }

      
        }
    }
}

