using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower
{
    public string Name;
    public int Cost;
    public GameObject prefab;

    public Tower(string _name, int _cost, GameObject _prefab)
    {
        Name = _name;
        Cost = _cost;
        prefab = _prefab;
    }
}
