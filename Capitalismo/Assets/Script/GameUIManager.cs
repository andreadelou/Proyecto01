using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;          // Texto para el dinero disponible
    public TextMeshProUGUI workersText;        // Texto para los obreros disponibles
    public TextMeshProUGUI assignedWorkersText; // Texto para los obreros asignados a tierras

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("⚠️ GameManager.Instance es NULL. Asegúrate de que GameManager está en la escena.");
        }
        else
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        moneyText.text = "Dinero: " + GameManager.Instance.money;
        workersText.text = "Obreros: " + GameManager.Instance.availableWorkers;
        assignedWorkersText.text = "Asignados: " + GameManager.Instance.assignedWorkers;
    }
}