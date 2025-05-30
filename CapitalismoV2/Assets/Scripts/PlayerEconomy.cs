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
            Debug.Log("Espacio");
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
        Vector3 rayOrigin = transform.position + (0.3f * Vector3.up);
        Debug.DrawRay(rayOrigin, Vector3.down, Color.red, 0.5f);

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 0.5f))
        {
            Debug.Log("Hit");
            GameObject tierra = hit.transform.gameObject;

            // Verifica si la tierra ya fue comprada
            if (tierrasCompradas.Contains(tierra))
            {
                return; // Si ya fue comprada, no hacer nada
            }

            int costo = 0;
            int ingreso = 0;

            // Determina el costo e ingreso basado en el tag del objeto
            if (tierra.CompareTag("Arena"))
            {
                Debug.Log("Arena");
                costo = 1;
                ingreso = 1;
            }
            else if (tierra.CompareTag("Madera"))
            {
                Debug.Log("Madera");
                costo = 3;
                ingreso = 1;
            }
            else if (tierra.CompareTag("Piedra"))
            {
                Debug.Log("Piedra");
                costo = 3;
                ingreso = 1;
            }
            else if (tierra.CompareTag("Oveja") )
            {
                Debug.Log("Oveja");
                costo = 5;
                ingreso = 1;
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

}
