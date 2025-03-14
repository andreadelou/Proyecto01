using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTile : Tile
{
    public float income = 3f;

    private void Start()
    {
        cost = 10;
        tileType = "City";
    }

    public override void OnClick()
    {
        Debug.Log("Has seleccionado una ciudad.");
        GameManager.Instance.AddMoney(income);
    }
}
