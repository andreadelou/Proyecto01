using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerEconomy : MonoBehaviour
{
    public int monedas = 7; // Monedas iniciales
    public TextMeshProUGUI monedasText; // Texto en UI para mostrar monedas

    private HashSet<GameObject> tierrasCompradas = new HashSet<GameObject>(); // Tierras ya compradas


    void Comprar()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
           if ( hit.transform.gameObject.CompareTag("Corn"))
            {
                Debug.Log("Corn");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            Comprar();
        }
    }
    /*
    private void Start()
    {
        ActualizarMonedasUI();
    }

    private void ActualizarMonedasUI()
    {
        if (monedasText != null)
        {
            monedasText.text = "Monedas: " + monedas;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tierrasCompradas.Contains(other.gameObject))
        {
            return; // Si ya fue comprada, no hacer nada
        }

        int costo = 0;
        int ingreso = 0;

        if (other.CompareTag("Corn"))
        {
            costo = 1;
            ingreso = 1;
        }
        else if (other.CompareTag("Coffe"))
        {
            costo = 3;
            ingreso = 4;
        }
        else if (other.CompareTag("Trade"))
        {
            costo = 5;
            ingreso = 10;
        }

        if (costo > 0 && monedas >= costo)
        {
            // Compra la tierra
            monedas -= costo;
            tierrasCompradas.Add(other.gameObject);
            ActualizarMonedasUI();
            StartCoroutine(GenerarIngresos(ingreso));
        }
    }

    private System.Collections.IEnumerator GenerarIngresos(int ingreso)
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Ingresa dinero cada 5 segundos
            monedas += ingreso;
            ActualizarMonedasUI();
        }
    }
    */
}
