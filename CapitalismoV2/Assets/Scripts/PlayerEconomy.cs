using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerEconomy : MonoBehaviour
{
    public int monedas = 7;
    public TextMeshProUGUI monedasText;
    public TextMeshProUGUI tierrasCompradasText;
    public TextMeshProUGUI objetivoText; // NUEVO: UI para el objetivo actual

    private HashSet<GameObject> tierrasCompradas = new HashSet<GameObject>();

    // Recursos
    private int maderas = 0;
    private int piedras = 0;
    private int ovejas = 0;

    // Estructuras
    private int casas = 0;
    private int molinos = 0;
    private int torres = 0;

    // Recompensas
    private const int RECOMPENSA_DEFAULT = 2;
    private const int RECOMPENSA_CASA = 5;
    private const int RECOMPENSA_MOLINO = 7;
    private const int RECOMPENSA_TORRE = 10;

    // Objetivo progresivo
    private int estadoObjetivo = 0; // 0: casas, 1: molino, 2: torres, 3: fin

    //Colores baldosas
    private Dictionary<GameObject, Color> tileOriginalColors = new Dictionary<GameObject, Color>();
    private Color lockedColor = Color.gray;

    //Nombre baldosa
    public TextMeshProUGUI tileInfoText;

    private void MostrarInfoBaldosa()
    {
        Vector3 rayOrigin = transform.position + (Vector3.up * 0.3f);
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 1f))
        {
            GameObject tile = hit.collider.gameObject;
            string tipo = tile.tag;

            // Opcional: renombrar tag a nombre amigable
            string nombre = tipo switch
            {
                "Madera" => "Madera ",
                "Piedra" => "Piedra ",
                "Oveja" => "Oveja ",
                "Casa" => "Casa (requiere 2 madera + 2 piedra)",
                "Molino" => "Molino (requiere 2 ovejas)",
                "Torres" => "Torre (2 casas + 1 molino)",
                "Arena" => "Arena (solo decorativa)",
                _ => "Desconocido"
            };

            tileInfoText.text = $"Tipo: {nombre}";
        }
        else
        {
            tileInfoText.text = "";
        }
    }

    private void Start()
    {
        ActualizarMonedasUI();
        ActualizarListaTierrasUI();
        ActualizarObjetivoUI();
        ColorearTodasLasTierras();
    }

    private void Update()
    {
        MostrarInfoBaldosa();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Comprar();
        }
    }

    private void ColorearTodasLasTierras()
    {
        // Puedes usar otros tags si tus tiles los tienen
        string[] tags = { "Arena", "Madera", "Piedra", "Oveja", "Casa", "Molino", "Torres" };

        foreach (string tag in tags)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject tile in tiles)
            {
                Renderer renderer = tile.GetComponent<Renderer>();
                if (renderer != null)
                {
                    tileOriginalColors[tile] = renderer.material.color;
                    renderer.material.color = lockedColor;
                }
            }
        }
    }

    private void ActualizarMonedasUI()
    {
        if (monedasText != null)
        {
            monedasText.text = "Monedas: " + monedas;
        }
    }

    private void ActualizarListaTierrasUI()
    {
        if (tierrasCompradasText != null)
        {
            tierrasCompradasText.text =
                $"Maderas: {maderas}\n" +
                $"Piedras: {piedras}\n" +
                $"Ovejas:  {ovejas}\n\n" +
                $"Casas:   {casas}\n" +
                $"Molinos: {molinos}\n" +
                $"Torres:  {torres}";
        }
    }

    private void ActualizarObjetivoUI()
    {
        if (objetivoText == null) return;

        switch (estadoObjetivo)
        {
            case 0:
                objetivoText.text = "Objetivo: Construye 2 casas \n 2 maderas, 2 piedras";
                break;
            case 1:
                objetivoText.text = "Objetivo: Construye 1 molino \n 2 ovejas ";
                break;
            case 2:
                objetivoText.text = "Objetivo: Construye 3 torres \n 2 casas, 1 molino";
                break;
            case 3:
                objetivoText.text = "¡Objetivo completo!";
                break;
        }
    }

    private void VerificarProgresoObjetivo()
    {
        switch (estadoObjetivo)
        {
            case 0:
                if (casas >= 2)
                {
                    estadoObjetivo = 1;
                    ActualizarObjetivoUI();
                }
                break;
            case 1:
                if (molinos >= 1)
                {
                    estadoObjetivo = 2;
                    ActualizarObjetivoUI();
                }
                break;
            case 2:
                if (torres >= 3)
                {
                    estadoObjetivo = 3;
                    ActualizarObjetivoUI();
                    CargarEscenaFinal();
                    CargarEscenaEnding();
                }
                break;
        }
    }

    private void Comprar()
    {
        Vector3 rayOrigin = transform.position + (0.3f * Vector3.up);
        Debug.DrawRay(rayOrigin, Vector3.down, Color.red, 0.5f);

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 0.5f))
        {
            GameObject tierra = hit.transform.gameObject;

            if (tierrasCompradas.Contains(tierra)) return;

            int costo = 0;
            int recompensa = 0;
            bool puedeComprar = false;

            // Recursos básicos
            if (tierra.CompareTag("Arena"))
            {
                costo = 3;
                if (monedas >= costo)
                {
                    monedas -= costo;
                    puedeComprar = true;
                }
            }
            if (tierra.CompareTag("Madera"))
            {
                costo = 3;
                if (monedas >= costo)
                {
                    monedas -= costo;
                    maderas++;
                    recompensa = RECOMPENSA_DEFAULT;
                    puedeComprar = true;
                }
            }
            else if (tierra.CompareTag("Piedra"))
            {
                costo = 3;
                if (monedas >= costo)
                {
                    monedas -= costo;
                    piedras++;
                    recompensa = RECOMPENSA_DEFAULT;
                    puedeComprar = true;
                }
            }
            else if (tierra.CompareTag("Oveja"))
            {
                costo = 5;
                if (monedas >= costo)
                {
                    monedas -= costo;
                    ovejas++;
                    recompensa = RECOMPENSA_DEFAULT;
                    puedeComprar = true;
                }
            }

            // Casa (requiere recursos)
            else if (tierra.CompareTag("Casa"))
            {
                if (maderas >= 2 && piedras >= 2)
                {
                    maderas -= 2;
                    piedras -= 2;
                    casas++;
                    recompensa = RECOMPENSA_CASA;
                    puedeComprar = true;
                }
            }
            // Molino
            else if (tierra.CompareTag("Molino"))
            {
                if (ovejas >= 2)
                {
                    ovejas -= 2;
                    molinos++;
                    recompensa = RECOMPENSA_MOLINO;
                    puedeComprar = true;
                }
            }
            // Torre
            else if (tierra.CompareTag("Torres"))
            {
                if (casas >= 2 && molinos >= 1)
                {
                    casas -= 2;
                    molinos -= 1;
                    torres++;
                    recompensa = RECOMPENSA_TORRE;
                    puedeComprar = true;
                }
            }

            if (puedeComprar)
            {
                tierrasCompradas.Add(tierra);
                //CAmbiar color tilde
                if (tileOriginalColors.TryGetValue(tierra, out Color originalColor))
                {
                    Renderer renderer = tierra.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = originalColor;
                    }
                }
                monedas += recompensa;
                ActualizarMonedasUI();
                ActualizarListaTierrasUI();
                VerificarProgresoObjetivo();
                if (monedas < 3)
                {
                    CargarEscenaEnding();
                }
            }
        }
    }

    private void CargarEscenaEnding()
    {
        SceneManager.LoadScene("Ending"); 
    }

    private void CargarEscenaFinal()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;

        if (siguienteEscena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteEscena);
        }
        else
        {
            Debug.Log("No hay una escena siguiente definida.");
        }
    }
}
