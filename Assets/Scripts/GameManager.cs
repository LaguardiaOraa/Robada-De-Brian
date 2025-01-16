using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    public static GameManager manager;

    //Creamos un singleton
    private void Awake()
    {
        // Comprueba si ya existe una instancia del objeto `manager` y si esta instancia no es la actual.
        if (manager != null && manager == this)
        {
            // Si se cumple la condición, destruye este objeto para evitar duplicados.
            Destroy(gameObject);
        }
        else 
        {
            // Si no existe otra instancia o esta instancia es diferente, asigna esta instancia como el `manager`.
            manager = this;
        }
    }

    public void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("MainMenu activated");
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("ItemsMenu activated");
    }

    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("ARPosition activated");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
