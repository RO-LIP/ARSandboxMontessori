using UnityEngine;

public interface ISpawnArea
{
    void SetSpawnable(MonoBehaviour spawnable);
    float GetCurrentEntitiesCount();

    float GetLowerBorder();

    float GetUpperBorder();

    GameObject Spawn(Vector3 position, Transform transform);
}