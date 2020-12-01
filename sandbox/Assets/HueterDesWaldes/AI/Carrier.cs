using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is a role of the class Villager. A carrier will gather water from a well and transport it to shoots.
 * The shoots are then able to grow into trees and improve upon the air quality.
 * @author Nicolas Durant
 */
public class Carrier : VillagerAlternate
{
    private SpawnArea forest;
    private GameObject currentTarget; //tree or well that is currently the target of movement
    private bool hasWaterInBucket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextTree(SpawnArea forest)
    {
        throw new NotImplementedException();
    }

    private void MoveToTree()
    {
        throw new NotImplementedException();
    }

    private void MoveToWell()
    {
        throw new NotImplementedException();
    }

    public void SetForest(SpawnArea forest)
    {
        throw new NotImplementedException();
    }
}
