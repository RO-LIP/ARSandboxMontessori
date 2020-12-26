using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparer : MonoBehaviour
{
    private int[][] errorTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int[][] MergeBitMaps (int[][] terrain, int[][] template)
    {
        int[][] mergedBitMaps = template;

        for(int i = 0; i < terrain.GetLength(0); i++)
        {
            for (int j = 0; j < terrain.GetLength(1); j++)
            {
                if(terrain[i][j] == 5)
                {
                    mergedBitMaps[i][j] = 5;
                }
            }
        }

        return mergedBitMaps;
    }

    private bool CompareShapes(int [][] mergedMaps)
    {
        bool shapeIdentity = true;

        //start only at 2nd column and go to 2nd to last coulmn to prevent arry out of bound exceptions; Problematic?
        for (int i = 1; i < mergedMaps.GetLength(0)-1; i++)
        {
            for (int j = 0; j < mergedMaps.GetLength(1); j++)
            {
                errorTiles[i][j] = 0;

                if (mergedMaps[i][j] == 5 && mergedMaps[i-1][j] == 0 && mergedMaps[i+1][j] == 0)
                {
                    shapeIdentity=false;
                    errorTiles[i][j] = 4;
                }
            }
        }

        return shapeIdentity;
    }

    public bool compare(int[][] input, int[][] template)
    {
        return CompareShapes(MergeBitMaps(input, template));
    }

    public int[][] GetErrorTiles()
    {
        return errorTiles;
    }
}
