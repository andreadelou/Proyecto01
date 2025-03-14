using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float money = 7f;  // Dinero inicial
    public int availableWorkers = 0; // Obreros disponibles
    public int assignedWorkers = 0;  // Obreros asignados a tierras

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddMoney(float amount)
    {
        money += amount;
        FindObjectOfType<GameUIManager>().UpdateUI();
    }

    public void AddWorker()
    {
        availableWorkers++;
        FindObjectOfType<GameUIManager>().UpdateUI();
    }

    public void AssignWorker()
    {
        if (availableWorkers > 0)
        {
            availableWorkers--;
            assignedWorkers++;
            FindObjectOfType<GameUIManager>().UpdateUI();
        }
    }
}
