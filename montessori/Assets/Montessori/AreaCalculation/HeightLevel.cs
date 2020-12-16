using System;
using System.Collections.Generic;

public enum HeightLevelType { UNDEFINED, WATER, VILLAGE, TREE, MOUNTAIN }

[Serializable]
public class HeightLevel
{
    public HeightLevelType type = HeightLevelType.UNDEFINED;
    public float lowerBorder = 0;
    public float upperBorder = 0;

    internal static HeightLevel GetFrom(List<HeightLevel> heights, float y)
    {
        HeightLevel height = new HeightLevel();

        foreach (HeightLevel current in heights)
        {
            float upperBorder = current.upperBorder;
            float lowerBorder = current.lowerBorder;
            //height level of position in grid matches current HeightLevel
            if (y <= upperBorder && y >= lowerBorder)
            {
                height = current;
            }
        }
        return height;
    }
}
