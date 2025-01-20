using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonMnanager itemButtonMnanager;

    void Start() 
    {
        GameManager.manager.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in items) 
        {
            ItemButtonMnanager itemButton;
            itemButton = Instantiate(itemButtonMnanager, buttonContainer.transform);
            itemButton.ItemName1 = item.name;
            itemButton.ItemImage1 = item.ItemImage;
            itemButton.ItemDescripcion1= item.ItemDescripcion;
            itemButton.Item3DModel1 = item.Item3DModel;
            itemButton.name = item.ItemName;
        }
    }

}
