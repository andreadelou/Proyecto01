using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detecta clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))  // Si golpea un objeto
            {
                Tile tile = hit.collider.GetComponent<Tile>(); // Busca el script "Tile"
                if (tile != null)
                {
                    if (GameManager.Instance.CanAfford(tile.cost))
                    {
                        GameManager.Instance.PurchaseTile(tile);
                        Debug.Log("Has comprado una casilla de tipo " + tile.tileType);
                    }
                    else
                    {
                        Debug.Log("No tienes suficiente dinero para comprar esta casilla.");
                    }
                }
            }
        }
    }
}