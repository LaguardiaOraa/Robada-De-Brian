using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArInteractionManager : MonoBehaviour
{
    [SerializeField] private Camera aRCamera;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject aRRPointer;
    private GameObject item3DModel;
    private GameObject sueloSOmbra;
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
            sueloSOmbra = value;
            sueloSOmbra.transform.position = aRRPointer.transform.position;
            sueloSOmbra.transform.parent = aRRPointer.transform;
            // isInitialPosition = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aRRPointer = transform.GetChild(0).gameObject;
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.manager.OnMainMenu += SetItemPosition;
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
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);
            if (touchOne.phase == TouchPhase.Began)
            {
                var touchPosition = touchOne.position;
                isOverUI = isTapOverUI(touchPosition);

                isOver3DModel = isTapOver3DModel(touchPosition);
            }
            if (touchOne.phase == TouchPhase.Moved)
            {
                if (aRRaycastManager.Raycast(touchOne.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    if (!isOverUI && isOver3DModel)
                    {
                        transform.position = hitPose.position;

                    }
                }

            }
            if (Input.touchCount == 2)
            {
                Touch touchTwo = Input.GetTouch(1);
                if (touchOne.phase == TouchPhase.Began || touchTwo.phase == TouchPhase.Began)
                {
                    initialTouchPos = touchTwo.position - touchOne.position;
                }
                if (touchOne.phase == TouchPhase.Moved || touchTwo.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = touchTwo.position - touchOne.position;
                    float angle = Vector2.SignedAngle(initialTouchPos, currentTouchPos);
                    item3DModel.transform.rotation = Quaternion.Euler(0, item3DModel.transform.eulerAngles.y - angle, 0);
                    initialTouchPos = currentTouchPos;
                }
            }
            if (isOver3DModel && item3DModel == null && !isOverUI)
            {
                GameManager.manager.ARPosition();
                item3DModel = itemSelected;
                itemSelected = null;
                aRRPointer.SetActive(true);
                transform.position = item3DModel.transform.position;
                item3DModel.transform.parent = aRRPointer.transform;
            }
        }
    }

    private bool isTapOver3DModel(Vector2 touchPosition)
    {
        Ray ray = aRCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            if (hit3DModel.collider.CompareTag("Item"))
            {

                itemSelected = hit3DModel.transform.gameObject;
                return true;
            }

        }
        return false;
    }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);

        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        return result.Count > 0;

    }
    private void SetFloorPosition()
    {
        if (sueloSOmbra != null)
        {
            sueloSOmbra.transform.parent = null;
        }
    }
    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            aRRPointer.SetActive(false);
            item3DModel = null;

        }
    }
    public void DeleteItem()
    {
        Destroy(item3DModel);
        aRRPointer.SetActive(false);
        GameManager.manager.MainMenu();
    }
}
