using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float money = 7f;  // Dinero inicial
    public int availableWorkers = 0; // Obreros disponibles
    public int assignedWorkers = 0;  // Obreros asignados a tierras

    private float timer = 0f;
    private float interval = 20f; // Intervalo de 20 segundos

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            GenerateIncome();
        }
    }

    private void GenerateIncome()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile is CityTile cityTile)
            {
                AddMoney(cityTile.income);
            }
            else if (tile is TradeTile tradeTile)
            {
                AddMoney(tradeTile.income);
            }
        }
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
            FindObjectOfType<GameUIManager>().UpdateUI(); // Actualiza la UI
        }
    }

    public bool CanAfford(int cost)
    {
        return money >= cost;
    }

    public void PurchaseTile(Tile tile)
    {
        if (CanAfford(tile.cost))
        {
            money -= tile.cost;
            FindObjectOfType<GameUIManager>().UpdateUI();
            Debug.Log("Has comprado una casilla de tipo " + tile.tileType);
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para comprar esta casilla.");
        }
    }
}