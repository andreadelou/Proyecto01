using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int cost;
    public string tileType;

    public virtual void OnClick()
    {
        Debug.Log($"Haz clic en una casilla de tipo {tileType}");
    }
}