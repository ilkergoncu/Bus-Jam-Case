using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Linq;
using System.Collections;

public class BusManager : MonoBehaviour
{
    public List<GameObject> busesPrefabs; 
    public Transform busSpawnPoint; 
    public Transform destinationPoint; 
    public Transform busExitPoint;
    public float moveDuration = 5.0f; 
    public int maxCapacity = 3; 

    private List<GameObject> activeBuses = new List<GameObject>(); 
    [SerializeField]
    private int currentBusIndex = 0; 
    public GameObject currentBus;

    public bool iscurrentBusReachedDestination;

    public GameObject busPrefab;

    public ColorType ColorType;
    public ColorDataSO ColorDataSo;

    public GameObject completePanel;

    bool isLevelCompleted;


    

    void Start()
    {
        if (busesPrefabs.Count > 0)
        {
            SpawnNextBus(); 
        }
    }



    public void CreateBus()
    {
        GameObject bus = Instantiate(busPrefab);
        bus.transform.position = new Vector3(transform.position.x - (busesPrefabs.Count * 2f), transform.position.y, transform.position.z);
        bus.transform.SetParent(transform);
        busesPrefabs.Add(bus);

        bus.GetComponent<CharacterAi>().ColorType = ColorType;
        bus.GetComponent<ControllerCharacterVisual>().SetCharacterMaterial(GetMaterialAccordingToColorType(ColorType));

    }

    private Material GetMaterialAccordingToColorType(ColorType colorType)
    {
        return ColorDataSo.ColorDatas.First(x => x.ColorType == colorType).Material;
    }

    public void RemoveBus()
    {

        DestroyImmediate(busesPrefabs[busesPrefabs.Count - 1]);
        busesPrefabs.Remove(busesPrefabs[busesPrefabs.Count - 1]);

    }

    public void SpawnNextBus()
    {
        if (currentBusIndex < busesPrefabs.Count)
        {

           
            MoveBusToDestination(busesPrefabs[currentBusIndex]);
            currentBus = busesPrefabs[currentBusIndex];
            currentBus.transform.position = this.transform.position;
            currentBusIndex++;
        }
      
    }

  

    void MoveBusToDestination(GameObject bus)
    {
        
        iscurrentBusReachedDestination = false;
     
        bus.transform.DOMove(destinationPoint.position, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            Debug.Log("Bus reached the destination.");
            iscurrentBusReachedDestination = true;
            //CheckBusCapacityAndManageNextBus(bus);
        });
    }

   public void CheckBusCapacityAndManageNextBus(GameObject bus)
    {



        Bus busComponent = bus.GetComponent<Bus>();
        if (busComponent != null && busComponent.IsFull)
        {
            if (bus.gameObject == busesPrefabs[busesPrefabs.Count - 1])
            {
                if (currentBusIndex >= busesPrefabs.Count && !isLevelCompleted)
                    StartCoroutine(CompleteLevel());
            }

            bus.gameObject.transform.DOMove(busExitPoint.position, moveDuration).OnComplete(() =>
            {
                Debug.Log("Bus reached the destination.");
              
              

               // CheckBusCapacityAndManageNextBus(bus);
            });
            activeBuses.Remove(bus);
            SpawnNextBus(); 
        }
    }

    public IEnumerator CompleteLevel()
    {
        isLevelCompleted = true;

        Camera.main.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        if (completePanel != null)
            completePanel.SetActive(true);
        completePanel.transform.localScale = Vector3.zero;
        completePanel.transform.DOScale(1, 1f).SetEase(Ease.OutBack);

        completePanel.SetActive(true);
        
    }
}