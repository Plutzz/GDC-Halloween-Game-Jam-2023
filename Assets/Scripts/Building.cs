using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Attributes")]
    public bool placed;
    public BoundsInt area;

    [Header("Tower Attributes")]
    public string Name;
    public int Cost;
    public GameObject prefab;

    public Building(string _name, int _cost, GameObject _prefab)
    {
        Name = _name;
        Cost = _cost;
        prefab = _prefab;
    }
    public bool getPlaced()
    {
        return placed;
    }

    private void setPlaced(bool p)
    {
        placed = p;
    }

    #region Building Methods

    public bool canBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.Instance.canTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }

    public void place()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        placed = true;
        GridBuildingSystem.Instance.takeArea(areaTemp);
    }

    public void setSprite()
    {

    }

    #endregion 
}