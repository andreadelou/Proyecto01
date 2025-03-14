using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTile : Tile
{
    public int workers = 4; // Cada casilla tiene 4 obreros

    private void Start()
    {
        cost = 3; // Los cuadrados valen 3 monedas
        tileType = "Worker";
    }

    public override void OnClick()
    {
        Debug.Log("Has seleccionado una casilla de obreros.");

        // Buscar una tierra cercana y asignar obreros
        FarmTile farm = FindNearbyFarm();
        if (farm != null && workers > 0)
        {
            farm.AddWorker();
            workers--;
            GameManager.Instance.AssignWorker(); // Actualiza el contador de obreros en GameManager
            Debug.Log("Se asignó un obrero. Obreros restantes: " + workers);
        }
        else if (workers <= 0)
        {
            Debug.Log("No hay obreros disponibles en esta casilla.");
        }
        else
        {
            Debug.Log("No hay tierras de cultivo cerca.");
        }
    }

    private FarmTile FindNearbyFarm()
    {
        // Busca tierras de cultivo cercanas 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            FarmTile farm = hitCollider.GetComponent<FarmTile>();
            if (farm != null)
            {
                return farm;
            }
        }
        return null;
    }
}