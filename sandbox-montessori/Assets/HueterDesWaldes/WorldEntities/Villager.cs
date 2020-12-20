using System;
using UnityEngine;

public class Villager : MonoBehaviour
{
    [SerializeField]
    private Interactable focusedTarget;

    [SerializeField] 
    private bool hasWaterInBucket;

    [SerializeField] 
    private float bucketCapacity;
    private SpawnArea forest;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Test move Villager to Tree and interact
        if (!focusedTarget.GetIsFocus())
            SetFocus(focusedTarget);
        
        if (!focusedTarget.GetHasInteracted())
            transform.position = Vector3.MoveTowards(transform.position, focusedTarget.transform.position, 10f * Time.deltaTime);
    }

    /// <summary>
    /// Sets the Focus on an interactable object.
    /// If there allready was a focused object, it gets defocused.
    /// </summary>
    /// <param name="newFocus">new interactable object to be focused</param>
    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focusedTarget)
        {
            if (focusedTarget != null)
                focusedTarget.OnDefocused();
            
            focusedTarget = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    /// <summary>
    /// Removes the focus of an interactable object.
    /// </summary>
    void RemoveFocus()
    {
        if (focusedTarget != null)
            focusedTarget.OnDefocused();

        focusedTarget = null;
    }

    /// <summary>
    /// Set the flag if villager has water in the bucket.
    /// </summary>
    /// <param name="hasWaterInBucket">flag if villager has water in the bucket</param>
    public void SetHasWaterInBucket(bool hasWaterInBucket)
    {
        this.hasWaterInBucket = hasWaterInBucket;
    }

    /// <summary>
    /// Get flag if villager has water in the bucket.
    /// </summary>
    /// <returns>flag if villager has water in the bucket</returns>
    public bool GetHaswaterInBucket()
    {
        return hasWaterInBucket;
    }

    /// <summary>
    /// Set the focused target.
    /// </summary>
    /// <param name="focusedTarget">the focused target</param>
    public void SetFocusedTarget(Interactable focusedTarget)
    {
        this.focusedTarget = focusedTarget;
    }

    /// <summary>
    /// Get the focused target.
    /// </summary>
    /// <returns>the focused target</returns>
    public Interactable GetFocusedTarget()
    {
        return focusedTarget;
    }

    /// <summary>
    /// Set the capacity of the bucket.
    /// </summary>
    /// <param name="bucketCapacity">the capacity of the bucket</param>
    public void SetBucketCapacity(float bucketCapacity)
    {
        this.bucketCapacity = bucketCapacity;
    }

    /// <summary>
    /// Get the capacity of the bucket.
    /// </summary>
    /// <returns>the capacity of the bucket</returns>
    public float GetBucketCapacity()
    {
        return bucketCapacity;
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
