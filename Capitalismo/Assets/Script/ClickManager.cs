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
                    tile.OnClick(); // Llama a la función de la casilla
                }
            }
        }
    }
}