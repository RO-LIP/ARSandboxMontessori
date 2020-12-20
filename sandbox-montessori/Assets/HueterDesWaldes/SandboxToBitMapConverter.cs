using Assets.HueterDesWaldes.AreaCalculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour, ISubscriber
{
    AreaCalculator areaCalculator;
    private int[][] bitMapDetcted;

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = FindObjectOfType<AreaCalculator>();
        areaCalculator.Attach(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConvertAreasToBitMap()
    {
        Assets.HueterDesWaldes.AreaCalculation.Grid grid = areaCalculator.GetGrid();

        int gridResolutionZ = grid.ResolutionZ;
        int gridResolutionX = grid.ResolutionX;
     
        //iterate over tiles in grid
        for (int x = 0; x < gridResolutionX; x++)
        {
            for (int z = 0; z < gridResolutionZ; z++)
            {
                Tile tileInGrid = grid.TileAt(x, z);
                HeightLevelType heightLevelType = tileInGrid.height.type;

                if (heightLevelType == HeightLevelType.WATER)
                    bitMapDetcted[x][z] = 5;
                bitMapDetcted[x][z] = 0;
            }
        }
    }

    public int[][] GetBitMapDetcted() 
    {
        return bitMapDetcted;
    }

    public void Notify()
    {
        ConvertAreasToBitMap();
        Debug.Log("Notified Converter");
    }
}
