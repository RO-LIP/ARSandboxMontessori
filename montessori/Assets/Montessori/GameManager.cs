using System;
using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;
using Assets.Montessori.BitmapConversion;
using Assets.Montessori.ColorCode;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject areaCalculatorGO;
    private IAreaCalculator areaCalculator;
    [SerializeField]
    private Comparer comparer;
    [SerializeField]
    private BitmapConverter bitmapConverter;
    [SerializeField]
    private ColorCodeProjector colorCodeProjector;
    [SerializeField]
    private SandboxToBitMapConverter sandboxToBitMapConverter;

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = areaCalculatorGO.GetComponent<IAreaCalculator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
