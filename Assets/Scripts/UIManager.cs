using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject ItemsMenuCanvas;
    [SerializeField] private GameObject ARPositionCanvas;
    
    public void Start()
    {
        GameManager.manager.OnMainMenu += ActivateMainMenu;
        GameManager.manager.OnItemsMenu += ActivateItemsMenu;
        GameManager.manager.OnARPosition += ActivateARPosition;
    }

    public void ActivateMainMenu()
    {
        MainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), .3f);
        MainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), .3f);
        MainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), .3f);

        ItemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, .3f);
        
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), .3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), .3f);
    }

    public void ActivateItemsMenu()
    {
        MainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), .3f);
        MainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), .3f);
        MainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), .3f);

        ItemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, .3f);
    }

    public void ActivateARPosition()
    {
        MainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), .3f);
        MainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), .3f);
        MainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), .3f);

        ItemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), .3f);
        ItemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, .3f);
        
        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), .3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), .3f);
    }
}
