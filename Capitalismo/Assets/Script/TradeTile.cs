using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeTile : Tile
{
    public float income = 1.5f;

    private void Start()
    {
        cost = 5;
        tileType = "Trade";
    }

    public override void OnClick()
    {
        Debug.Log("Has seleccionado una casilla de comercio.");
        GameManager.Instance.AddMoney(income);
    }
}
