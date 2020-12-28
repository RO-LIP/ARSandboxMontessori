using System.Collections.Generic;

namespace Assets.HueterDesWaldes.AreaCalculation
{
    public class DynamicHeightLevel : HeightLevel
    {
        public static List<HeightLevel> DynamicBorders(Grid grid)
        {
            List<HeightLevel> dynamicBorders = new List<HeightLevel>();
            //sort y-values in grid
            SortedSet<float> yValues = new SortedSet<float>();
            foreach (Tile tile in grid.Tiles)
                yValues.Add(tile.WorldPos.y);
            //save max-value for HI heightLevel.upperBorder
            float max = yValues.Max;
            //remove upper 10% values to eradicate spikes
            int count = yValues.Count;
            for (int i = 0; i < count * 0.1; i++)
                yValues.Remove(yValues.Max);
            //get mathematical average to divide heightLevels
            float sum = 0;
            foreach (float value in yValues)
                sum += value;
            float average = sum / yValues.Count;
            //create two heightLevels that are divided by the average y-value (of p0.9-quantile)
            dynamicBorders.Add(new HeightLevel
            {
                type = HeightLevelType.LO,
                lowerBorder = 0,
                upperBorder = average
            });
            dynamicBorders.Add(new HeightLevel
            {
                type = HeightLevelType.HI,
                lowerBorder = average,
                upperBorder = max
            });
            return dynamicBorders;
        }
    }
}
