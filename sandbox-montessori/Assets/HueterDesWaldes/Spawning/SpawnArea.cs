using Assets.HueterDesWaldes.AreaCalculation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : ISpawnArea
{
    //static allowed entities per height level
    static float TREESPERSQUAREMETER = 0.3f;
    static float WELLSPERSQUAREMETER = 0.3f;
    static float HOUSESPERSQUAREMETER = 0.3f;

    [SerializeField]
    float allowedEntitiesPerSquareMeter;
    float areaLevelUpperBorder;
    float areaLevelLowerBorder;
    int currentEntities;
    Area area;
    MonoBehaviour spawnable;

    [SerializeField]
    GameObject treePrefab;
    [SerializeField]
    GameObject wellPrefab;
    [SerializeField]
    GameObject housePrefab;

    #region Konstruktoren
    public SpawnArea()
    {;}
    public SpawnArea (Area a, float lowerB, float upperB)
    {
        area = a;
        areaLevelLowerBorder = lowerB;
        areaLevelUpperBorder = upperB;
    }
    public SpawnArea(Area a, float lowerB, float upperB, int entities)
    {
        area = a;
        areaLevelLowerBorder = lowerB;
        areaLevelUpperBorder = upperB;
        currentEntities = entities;
    }
    #endregion

    #region  Setter
    public void SetSpawnable(MonoBehaviour spawnable)
    {
        this.spawnable = spawnable;
    }
    public void SetCurrentEntitiesCount(int count)
    {
        currentEntities = count;
    }
    public void SetAllowedEntitiesForArea()
    {
        Type t = spawnable.GetType();

        if (t == typeof(Tree))
            allowedEntitiesPerSquareMeter = TREESPERSQUAREMETER;
        else if (t == typeof(Well))
            allowedEntitiesPerSquareMeter = WELLSPERSQUAREMETER;
        else if (t == typeof(House))
            allowedEntitiesPerSquareMeter = HOUSESPERSQUAREMETER;
        else
            allowedEntitiesPerSquareMeter = 0.0f;
    }
    #endregion

    #region Getter
    public float GetCurrentEntitiesCount()
    {
        return currentEntities;
    }
    public Area GetArea()
    {
        return area;
    }
    public float GetLowerBorder()
    {
        throw new System.NotImplementedException();
    }

    public float GetUpperBorder()
    {
        throw new System.NotImplementedException();
    }
    public MonoBehaviour GetSpawnable()
    {
        return spawnable;
    }
    #endregion

    #region Spanwlogik
    public bool SpawnNewEntityAllowed()
    {
        return ((area.SizeInSquareMeters() * allowedEntitiesPerSquareMeter) - currentEntities >= 1.0f) ? true : false;
    }

    public bool DeSpawnEntityRequired()
    {
        return ((area.SizeInSquareMeters() * allowedEntitiesPerSquareMeter) - currentEntities < 0.0f) ? true : false;        
    }

    public GameObject Spawn(Vector3 position, Transform transform)
    {
        GameObject prefab = null;

        if (spawnable.GetType() == typeof(House))
            prefab = housePrefab;
        else if (spawnable.GetType() == typeof(Tree))
            prefab = treePrefab;
        else if (spawnable.GetType() == typeof(Well))
            prefab = wellPrefab;
        
        GameObject newEntity = GameObject.Instantiate(prefab, position, Quaternion.identity, transform);
        return newEntity;
    }
    #endregion

}
