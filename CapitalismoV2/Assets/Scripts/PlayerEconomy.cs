using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerEconomy : MonoBehaviour
{
    public int monedas = 7; // Monedas iniciales
    public TextMeshProUGUI monedasText; // Texto en UI para mostrar monedas
    public TextMeshProUGUI tierrasCompradasText; //Tierras compradas
    public string Ending = "Ending";

    private HashSet<GameObject> tierrasCompradas = new HashSet<GameObject>(); // Tierras ya compradas

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Comprar();
        }
    }

    private void Start()
    {
        ActualizarMonedasUI();
        ActualizarListaTierrasUI();
    }

    private void ActualizarMonedasUI()
    {
        if (monedasText != null)
        {
            monedasText.text = "Monedas: " + monedas;
        }
        // Verificar si alcanzó las monedas deseadas
        if (monedas >= 80)
        {
            CargarEscenaFinal();
        }
    }

    private void CargarEscenaFinal()
    {
        SceneManager.LoadScene(Ending);
    }


    private void ActualizarListaTierrasUI()
    {
        if (tierrasCompradasText != null)
        {
            // Crear una lista de nombres de las tierras compradas
            List<string> nombresTierras = tierrasCompradas.Select(t => t.name).ToList();

            // Mostrar los nombres en el texto de la UI
            tierrasCompradasText.text = "Own:\n" + string.Join("\n", nombresTierras);
        }
    }

    void Comprar()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            GameObject tierra = hit.transform.gameObject;

            // Verifica si la tierra ya fue comprada
            if (tierrasCompradas.Contains(tierra))
            {
                return; // Si ya fue comprada, no hacer nada
            }

            int costo = 0;
            int ingreso = 0;

            // Determina el costo e ingreso basado en el tag del objeto
            if (tierra.CompareTag("Corn"))
            {
                Debug.Log("Corn");
                costo = 1;
                ingreso = 1;
            }
            else if (tierra.CompareTag("Coffe"))
            {
                Debug.Log("Coffe");
                costo = 3;
                ingreso = 4;
            }
            else if (tierra.CompareTag("Trade"))
            {
                Debug.Log("Trade");
                costo = 5;
                ingreso = 10;
            }

            // Si el costo es mayor que 0 y tienes suficientes monedas, compra la tierra
            if (costo > 0 && monedas >= costo)
            {
                monedas -= costo;
                tierrasCompradas.Add(tierra);
                ActualizarMonedasUI();
                ActualizarListaTierrasUI();
                StartCoroutine(GenerarIngresos(ingreso));
            }
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


    /*
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
