using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool placed;
    public BoundsInt area;

    void Start()
    {

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
        Debug.Log("Take Area");
        GridBuildingSystem.Instance.takeArea(areaTemp);
    }

    public void setSprite()
    {

    }

    #endregion 
}