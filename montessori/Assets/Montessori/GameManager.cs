using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;
using Assets.Montessori.BitmapConversion;
using Assets.Montessori.ColorCode;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject areaCalculatorGO;
    private AreaCalculator areaCalculator;
    [SerializeField]
    private Comparer comparer;
    [SerializeField]
    private BitmapConverter bitmapConverter;
    [SerializeField]
    private ColorCodeProjector colorCodeProjector;
    [SerializeField]
    private SandboxToBitMapConverter sandboxToBitMapConverter;

    private void Awake()
    {
        //resolve (cross-)dependencies of Montessori-Objects
        comparer.bitmapConverter = bitmapConverter;
        comparer.sandboxToBitMapConverter = sandboxToBitMapConverter;
        comparer.areaCalculator = areaCalculator;
        colorCodeProjector.i_areaCalculator = areaCalculator;
        colorCodeProjector.publisher = comparer;
        colorCodeProjector.source = comparer;
        sandboxToBitMapConverter.areaCalculator = areaCalculator;
    }

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = areaCalculatorGO.GetComponent<AreaCalculator>();
    }
}
