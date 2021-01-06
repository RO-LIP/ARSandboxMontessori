using Assets.HueterDesWaldes.AreaCalculation;
using UnityEngine;

public class SandboxToBitMapConverter : MonoBehaviour, ISubscriber
{
    public AreaCalculator areaCalculator { private get; set; }
    private int[,] bitMapDetected;

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = FindObjectOfType<AreaCalculator>();
        areaCalculator.Attach(this);
    }

    private void ConvertAreasToBitMap()
    {
        Assets.HueterDesWaldes.AreaCalculation.Grid grid = areaCalculator.GetGrid();
        int cnt0 = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        int cnt5 = 0;



        int gridResolutionZ = grid.ResolutionZ;
        int gridResolutionX = grid.ResolutionX;

        bitMapDetected = new int[gridResolutionX, gridResolutionZ];

        //iterate over tiles in grid
        for (int x = 0; x < gridResolutionX; x++)
        {
            for (int z = 0; z < gridResolutionZ; z++)
            {
                Tile tileInGrid = grid.TileAt(x, z);
                HeightLevelType heightLevelType = tileInGrid.HeightLevel.type;

                bitMapDetected[x, z] = 0;
                if (heightLevelType == HeightLevelType.LO)
                    bitMapDetected[x, z] = 5;
                //DEBUG BEGIN
                if (bitMapDetected[x, z] == 0) cnt0++;
                if (bitMapDetected[x, z] == 1) cnt1++;
                if (bitMapDetected[x, z] == 2) cnt2++;
                if (bitMapDetected[x, z] == 5) cnt5++;
                //DEBUG END
            }
        }
        Debug.Log("Value-Count SANDBOX: 0=" + cnt0 + " 1=" + cnt1 + " 2=" + cnt2 + " 5=" + cnt5);
    }

    public int[,] GetBitMapDetcted()
    {
        return bitMapDetected;
    }

    public void Notify(bool result = false)
    {
        ConvertAreasToBitMap();
        Debug.Log("Notified Converter");
    }
}