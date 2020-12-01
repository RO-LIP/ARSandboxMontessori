using UnityEngine;

public class Tree : Interactable, IAirQualityParameter, ICountdownTimer
{
    [SerializeField]
    private float timer;

    [SerializeField]
    private float amountWater;

    [SerializeField]
    private float threshholdWaterForGrowth;

    [SerializeField]
    private bool isFullyGrown;

    /// <summary>
    /// Initialize the tree as sprout without water and having a certain threshhold to grow.
    /// Start timer with central CountdownTimer value.
    /// </summary>
    void Start()
    {
        amountWater = 0;
        threshholdWaterForGrowth = 10;
        isFullyGrown = false;

        ResetTimer(CountdownTimer.GetCountdownInSec());
    }

    /// <summary>
    /// Interacting with the tree.
    /// Check if Villager has water in the bucket before waering the tree.
    /// </summary>
    public override void Interact()
    {
        base.Interact();

        Villager villager = GetActor().GetComponent<Villager>();
        if (villager.GetHaswaterInBucket())
            Water(villager.GetBucketCapacity());
    }

    /// <summary>
    /// Override the base Update method. Update is called once per frame.
    /// Check timer and change airQuality. Reset timer afterwards.
    /// </summary>
    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeAirQuality(+0.001f);
            ResetTimer(CountdownTimer.GetCountdownInSec());
        }
    }

    /// <summary>
    /// Changes the overall AirQuality by a given value.
    /// </summary>
    /// <param name="value">value to cahnge the overall airQuality</param>
    public void ChangeAirQuality(float value)
    {
        AirQuality.airQuality += value;
    }

    /// <summary>
    /// Resets the timer to the given countdownInSec.
    /// </summary>
    /// <param name="countdownInSec">countdownInSec</param>
    public void ResetTimer(float countdownInSec)
    {
        timer = countdownInSec;
    }

    /// <summary>
    /// Watering the tree with water fills the amount of water for the tree.
    /// The tree grows if the threshhold is reached.
    /// </summary>
    /// <param name="water">amount of water</param>
    public void Water(float water)
    {
        amountWater += water;
        if (isFullyGrown == false && amountWater >= threshholdWaterForGrowth)
            Grow();
    }

    /// <summary>
    /// Tree grows from sprout to a fully grown tree.
    /// </summary>
    private void Grow()
    {
        isFullyGrown = true;
    }

    /// <summary>
    /// Set the amount of water.
    /// </summary>
    /// <param name="amountWater">amount of water</param>
    public void SetAmountWater(float amountWater)
    {
        this.amountWater = amountWater;
    }

    /// <summary>
    /// Get the current amount of water.
    /// </summary>
    /// <returns>amount of water</returns>
    public float GetAmountWater()
    {
        return amountWater;
    }

    /// <summary>
    /// Set the threshhold of water for the tree to grow.
    /// </summary>
    /// <param name="threshholdWaterForGrowth">threshhold of water for the tree to grow</param>
    public void SetThreshholdWaterForGrowth(float threshholdWaterForGrowth)
    {
        this.threshholdWaterForGrowth = threshholdWaterForGrowth;
    }

    /// <summary>
    /// Get the threshhold of water for the tree to grow.
    /// </summary>
    /// <returns>threshhold of water for the tree to grow</returns>
    public float GetThreshholdWaterForGrowth()
    {
        return threshholdWaterForGrowth;
    }

    /// <summary>
    /// Set the flag for the tree to be fully grown.
    /// </summary>
    /// <param name="isFullyGrown">flag for the tree to be fully grown</param>
    public void SetIsFullyGrown(bool isFullyGrown)
    {
        this.isFullyGrown = isFullyGrown;
    }

    /// <summary>
    /// Get the flag for the tree to be fully grown.
    /// </summary>
    /// <returns>flag for the tree to be fully grown</returns>
    public bool GetIsFullyGrown()
    {
        return isFullyGrown;
    }
}