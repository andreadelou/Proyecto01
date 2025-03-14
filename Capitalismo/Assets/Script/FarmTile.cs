using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : Tile
{
    public int requiredWorkers = 5; // Se necesitan 5 obreros para trabajar la tierra
    public int currentWorkers = 0;  // Obreros actualmente asignados
    public float income = 2f;      // Ingreso si hay suficientes obreros

    private void Start()
    {
        cost = 1; // Las tierras valen 1 moneda
        tileType = "Farm";
    }

    public override void OnClick()
    {
        Debug.Log("Has seleccionado una tierra de cultivo.");

        if (currentWorkers >= requiredWorkers)
        {
            Debug.Log("Producción activada. Genera " + income + " monedas.");
            GameManager.Instance.AddMoney(income);
        }
        else
        {
            Debug.Log("No hay suficientes obreros. Se necesitan " + requiredWorkers + ".");
        }
    }

    public void AddWorker()
    {
        currentWorkers++;
        Debug.Log("Se agregó un obrero. Total: " + currentWorkers);
    }
}