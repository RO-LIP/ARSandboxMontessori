using UnityEngine;

namespace Assets.HueterDesWaldes.WorldEntities
{
    public class Tree : Interactable, IAirQualityParameter, ICountdownTimer
    {
        [SerializeField]
        private float timer;

        [SerializeField]
        private float amountWater;

        [SerializeField]
        private float threshholdWaterForGrowth;

        [SerializeField]
        private float airQualityImpactAsSprout;

        [SerializeField]
        private float airQualityImpactAsTree;


        /// <summary>
        /// Start timer with central CountdownTimer value.
        /// </summary>
        void Start()
        {
            ResetTimer(CountdownTimer.GetCountdownInSec());
        }

        /// <summary>
        /// Interacting with the tree.
        /// Check if villager has water in the bucket before watering the tree.
        /// Set HasWaterInBucket to false.
        /// </summary>
        /// <param name="villager">villager who is interacting</param>
        public override void Interact(Villager villager)
        {
            base.Interact(villager);
            if (villager.GetHaswaterInBucket())
                Water(villager.GetBucketCapacity());
            villager.SetHasWaterInBucket(false);
        }

        /// <summary>
        /// Update is called once per frame.
        /// Check timer and change airQuality. Reset timer afterwards.
        /// </summary>
        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (GetInteractableType() == InteractableType.SPROUT)
                    ChangeAirQuality(airQualityImpactAsSprout);
                else
                    ChangeAirQuality(airQualityImpactAsTree);

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
        /// Watering the tree or sprout with water fills the amount of water.
        /// The sprout grows if the threshhold is reached.
        /// </summary>
        /// <param name="water">amount of water</param>
        public void Water(float water)
        {
            amountWater += water;
            if (GetInteractableType() == InteractableType.SPROUT && amountWater >= threshholdWaterForGrowth)
                Grow();
        }

        /// <summary>
        /// Tree grows from sprout to a fully grown tree.
        /// </summary>
        private void Grow()
        {
            SetInteractableType(InteractableType.TREE);
            transform.localScale = new Vector3(4f, 4f, 4f);
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
    }
}