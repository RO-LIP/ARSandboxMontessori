using System.Collections.Generic;
using UnityEngine;
using Assets.Montessori.ColorCode;
using Assets.HueterDesWaldes.AreaCalculation;
using Assets.Montessori.BitmapConversion;

public class Comparer : MonoBehaviour, ISubscriber, IPublisher, IColorCodeSource
{
    public BitmapConverter bitmapConverter { private get; set; }
    public SandboxToBitMapConverter sandboxToBitMapConverter { private get; set; }
    public AreaCalculator areaCalculator { private get; set; }
    private Assets.Montessori.ColorCode.Color[,] colorTiles;

    private bool shapeIdentity;
    private readonly List<ISubscriber> subscribers = new List<ISubscriber>();
    [SerializeField]
    private int tolerance = 2;

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator.Attach(this);
    }

    private int[,] MergeBitMaps(int[,] terrain, int[,] template)
    {
        AddTolerance(template);
        int[,] mergedBitMaps = template;

        for (int i = 0; i < terrain.GetLength(0); i++)
        {
            for (int j = 0; j < terrain.GetLength(1); j++)
            {
                if (terrain[i, j] == 5)
                {
                    mergedBitMaps[i, j] = 5;
                }
            }
        }

        return mergedBitMaps;
    }

    private void AddTolerance(int[,] template)
    {
        for (int i = 0; i < template.GetLength(0); i++)
        {
            for (int j = 0; j < template.GetLength(1); j++)
            {
                if (template[i, j] == 1)
                {
                    for (int k = -tolerance; k < tolerance; k++)
                    {
                        for (int l = -tolerance; l < template.GetLength(1); l++)
                        {
                            bool columnInBitMap = (i + k >= 0) && (i + k < template.GetLength(0));
                            bool rowInBitMap = (j + l >= 0) && (j + l < template.GetLength(1));
                            if (columnInBitMap && rowInBitMap && template[i + k, j + l] == 0)
                            {
                                template[i + k, j + l] = 2;
                            }
                        }
                    }
                }
            }
        }
    }

    private bool CompareShapes(int[,] mergedMaps)
    {
        bool shapeIdentity = true;
        PrefillColorTiles(mergedMaps);

        //start only at 2nd column and go to 2nd to last coulmn to prevent arry out of bound exceptions; Problematic?
        for (int i = 1; i < (mergedMaps.GetLength(0) - 1); i++)
        {
            for (int j = 0; j < mergedMaps.GetLength(1); j++)
            {
                colorTiles[i, j] = 0;

                if (mergedMaps[i, j] == 0)
                {
                    colorTiles[i, j] = Assets.Montessori.ColorCode.Color.UNDEFINED;
                }

                else if (mergedMaps[i, j] == 1)
                {
                    colorTiles[i, j] = Assets.Montessori.ColorCode.Color.RED;

                    int extend = 1;
                    while((mergedMaps[i - extend, j] != 0) && (mergedMaps[i + extend, j] != 0))
                    {
                        if((mergedMaps[i - extend, j] == 5) || (mergedMaps[i + extend, j] == 5))
                        {
                            colorTiles[i, j] = Assets.Montessori.ColorCode.Color.GREEN;
                            break;
                        }

                        extend++;
                    }
                }

                else if (mergedMaps[i, j] == 5 && mergedMaps[i - 1, j] == 0 && mergedMaps[i + 1, j] == 0)
                {
                    shapeIdentity = false;
                    colorTiles[i, j] = Assets.Montessori.ColorCode.Color.RED;
                }

            }
        }

        return shapeIdentity;
    }

    private void PrefillColorTiles(int[,] mergedMaps)
    {
        colorTiles = new Assets.Montessori.ColorCode.Color[mergedMaps.GetLength(0), mergedMaps.GetLength(1)];

        for (int i = 0; i < mergedMaps.GetLength(0); i++)
        {
            for (int j = 0; j < mergedMaps.GetLength(1); j++)
            {
                if (i == 0 || i == (mergedMaps.GetLength(0) - 1))
                {
                    colorTiles[i, j] = Assets.Montessori.ColorCode.Color.UNDEFINED;
                }

                else
                {
                    colorTiles[i, j] = Assets.Montessori.ColorCode.Color.GREEN;
                }
            }
        }
    }

    private bool compare(int[,] input, int[,] template)
    {
        return CompareShapes(MergeBitMaps(input, template));
    }

    public Assets.Montessori.ColorCode.Color[,] GetColorArray()
    {
        return colorTiles;
    }

    public void Notify(bool result = false)
    {
        int[,] template = bitmapConverter.GetBitmapConverted();
        int[,] terrain = sandboxToBitMapConverter.GetBitMapDetcted();
        shapeIdentity = compare(terrain, template);
        NotifySubscibers();
    }

    public void Attach(ISubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void Detach(ISubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void NotifySubscibers()
    {
        foreach (ISubscriber subscriber in subscribers)
            subscriber.Notify(shapeIdentity);
    }
}