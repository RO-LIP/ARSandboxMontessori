using Assets.HueterDesWaldes.AreaCalculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TestSubscriber has to be attached as Component to the AreaCalculator GameObject / Prefab
/// </summary>
public class TestSubscriber : MonoBehaviour, ISubscriber
{
    private AreaCalculator areaCalculator;

    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = transform.GetComponentInParent<AreaCalculator>();
        areaCalculator.Attach(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Notify()
    {
        areaCalculator.Calculate();
    }
}
