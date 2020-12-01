using UnityEngine;

public class Interactable : MonoBehaviour
{
    // radius for interactable objects to interacted with
    [SerializeField]
    float radius = 3f;

    // flag if the interactable is focused by an actor
    [SerializeField]
    bool isFocus = false;
    Transform actor;

    // flag if an interaction has occured, so that actors can interact only once per focus
    [SerializeField]
    bool hasInteracted = false;

    /// <summary>
    /// Interact method to be overwritten.
    /// </summary>
    public virtual void Interact()
    {
        // This method is meant to be overwritten!
        //Debug.Log($"Interactng with {transform.name}");
    }

    /// <summary>
    /// This is called per frame.
    /// Checking if the actor has focused the interactable.
    /// Checking if the actor is in the radius of the interactable.
    /// If bth is true the Interact Method gets called.
    /// </summary>
    public virtual void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(actor.position, transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    /// <summary>
    /// Focus the interactable and set its flags.
    /// </summary>
    /// <param name="actorTransform">actor as Transform object</param>
    public void OnFocused(Transform actorTransform)
    {
        isFocus = true;
        actor = actorTransform;
        hasInteracted = false;
    }

    /// <summary>
    /// Defocus the interactable and reset its flags.
    /// </summary>
    /// <param name="actorTransform">actor as Transform object</param>
    public void OnDefocused()
    {
        isFocus = false;
        actor = null;
        hasInteracted = false;
    }

    /// <summary>
    /// Enables the object to get a visible wireframe for the interaction radius in the editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    /// <summary>
    /// Set the radius for interacting with the object.
    /// </summary>
    /// <param name="radius">radius for interacting</param>
    public void SetRadius(float radius)
    {
        this.radius = radius;
    }

    /// <summary>
    /// Get the radius for interacting with the object.
    /// </summary>
    /// <returns>radius for interacting</returns>
    public float GetRadius()
    {
        return radius;
    }

    /// <summary>
    /// Set flag if the object is focused.
    /// </summary>
    /// <param name="isFocus">flag if the object is focused</param>
    public void SetIsFocus(bool isFocus)
    {
        this.isFocus = isFocus;
    }

    /// <summary>
    /// Get flag if the object is focused.
    /// </summary>
    /// <returns>flag if the object is focused</returns>
    public bool GetIsFocus()
    {
        return isFocus;
    }

    /// <summary>
    /// Set the actor for interacting with the object.
    /// </summary>
    /// <param name="actor">the actor for interacting</param>
    public void SetActor(Transform actor)
    {
        this.actor = actor;
    }

    /// <summary>
    /// Get the actor for interacting with the object.
    /// </summary>
    /// <returns>the actor for interacting</returns>
    public Transform GetActor()
    {
        return actor;
    }

    /// <summary>
    /// Set the flag if the object has been interacted with.
    /// </summary>
    /// <param name="hasInteracted">flag if the object has been interacted with</param>
    public void SetHasInteracted(bool hasInteracted)
    {
        this.hasInteracted = hasInteracted;
    }

    /// <summary>
    /// Get the flag if the object has been interacted with.
    /// </summary>
    /// <returns>flag if the object has been interacted with</returns>
    public bool GetHasInteracted()
    {
        return hasInteracted;
    }
}
