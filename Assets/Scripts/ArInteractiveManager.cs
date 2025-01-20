using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArInteractiveManager : MonoBehaviour
{
    [SerializeField] private Camera aRCamera;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject aRRPointer;
    private GameObject item3DModel;
    private GameObject sueloSombra;
    private GameObject itemSelected;
    private bool isInitialPosition;
    private bool isOverUI;
    private bool isOver3DModel;
    private Vector2 initialTouchPos;

    public GameObject Item3DModel 
    {
        set 
        {
            item3DModel = value;
            item3DModel.transform.position = aRRPointer.transform.position;
            item3DModel.transform.parent = aRRPointer.transform;
            isInitialPosition = true;
        }
    }

    public GameObject SueloSombra
    {
        set
        {
            sueloSombra = value;
            sueloSombra.transform.position = aRRPointer.transform.position;
            sueloSombra.transform.parent = aRRPointer.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialPosition)
        {
            Vector2 midlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            aRRaycastManager.Raycast(midlePointScreen, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                aRRPointer.SetActive(true);
                isInitialPosition = false;
                SetFloorPosition();

            }
        }
    }

    private void SetFloorPosition() {

    }

    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRRPointer.SetActive(false);
            Item3DModel = null;
        }
    }

    public void DeleItem()
    {
        Destroy(item3DModel);
        aRRPointer.SetActive(true);
        GameManager.manager.MainMenu();
    }
}
