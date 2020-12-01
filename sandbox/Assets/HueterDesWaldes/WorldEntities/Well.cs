public class Well : Interactable
{
    /// <summary>
    /// Interacting with the well gives the interacting villager water as flag hasWaterInBucket.
    /// </summary>
    public override void Interact()
    {
        base.Interact();
        Villager villager = GetActor().GetComponent<Villager>();
        villager.SetHasWaterInBucket(true);
    }
}
