using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;
using System.Linq;

public class Spawner : MonoBehaviour, ISubscriber
{
    Dictionary<Tile, GameObject> entitiesPosition = new Dictionary<Tile, GameObject>();
    AreaCalculator areaCalculator;
    List<SpawnArea> spawnAreas;


    // Start is called before the first frame update
    void Start()
    {
        areaCalculator = FindObjectOfType<AreaCalculator>(); 
        areaCalculator.Attach(this);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}

    #region Setup new SpawnAreas
    private void CalculateSpawnAreas()
    {
        spawnAreas = new List<SpawnArea>();
        areaCalculator.Calculate();

        foreach (HeightLevelType levelType in Enum.GetValues(typeof(HeightLevelType)))
        {
            //alle Flächen auf einem Höhenlevel bestimmen          
            List<Area> areas = areaCalculator.ContiguousAreasOnHeightLevel(levelType);
            //aus den Flächen die SpawnAreas generieren
            if(areas != null && levelType != HeightLevelType.UNDEFINED)
            {
                IEnumerable<SpawnArea> spawnAreasOnHeightLevel = SetupSpawnAreasForHeightLevel(areas, levelType);
                spawnAreas.AddRange(spawnAreasOnHeightLevel);
            }
        }     
    }
    #endregion

    #region Hilfsmethoden Setup SpawnArea
    private IEnumerable<SpawnArea> SetupSpawnAreasForHeightLevel (List<Area> areas, HeightLevelType levelType)
    {
        IEnumerable<SpawnArea> spawnAreas = new List<SpawnArea>();

        //obere und untere Grenze des Höhenleveltyps bestimmen
        float[] borders = GetHeightLevelBorders(levelType);

        foreach (Area area in areas)
        {
            int entityCount = CountEntitiesInArea(area);
            SpawnArea s = new SpawnArea(area, borders[0], borders[1], entityCount);
            SetSpawnableInSpawnArea(s, levelType);
            spawnAreas.Append(s);
        }
        return spawnAreas;
    }

    private float[] GetHeightLevelBorders (HeightLevelType levelType)
    {
        float[] borders = new float[2];

        if(levelType != HeightLevelType.UNDEFINED)
        {
            List<HeightLevel> levels = areaCalculator.GetHeightLevels();
            borders[0] = levels.Where(a => a.type == levelType).FirstOrDefault().lowerBorder;
            borders[1] = levels.Where(a => a.type == levelType).FirstOrDefault().upperBorder;
        }
        return borders;
    }

    private static void SetSpawnableInSpawnArea (SpawnArea area, HeightLevelType levelType)
    {
        MonoBehaviour spawnable = new MonoBehaviour();

        if (levelType == HeightLevelType.VILLAGE)
            spawnable = new House();
        else if (levelType == HeightLevelType.TREE)
            spawnable = new Tree();
        else if (levelType == HeightLevelType.WATER)
            spawnable = new Well();

        area.SetSpawnable(spawnable);
        //Anzahl erlaubte entities hier gleich mitsetzen
        area.SetAllowedEntitiesForArea();
    }

    private int CountEntitiesInArea(Area area)
    {
        int entities = 0;
        foreach (KeyValuePair<Tile, GameObject> entry in entitiesPosition)
        {
            if (area.Contains(entry.Key))
                entities++;          
        }
        return entities;
    }
    #endregion

    #region Spawning und DeSpawning für komplette Welt
    private void SpawnEntity(SpawnArea spawnArea)
    {
        //TODO check for unity.random
        System.Random rnd = new System.Random();
        SortedSet<Tile> tiles = spawnArea.GetArea().GetAllTiles();
        IEnumerable<Tile> tilesWithEntities = entitiesPosition.Keys.AsEnumerable<Tile>();

        tiles.ExceptWith(tilesWithEntities);  
        Tile spawningTile = tiles.ElementAt(rnd.Next((tiles.Count() - 1)));

        GameObject newEntity = spawnArea.Spawn(spawningTile.GetWorldPos(), transform);

        entitiesPosition.Add(spawningTile, newEntity);
    }

    private void DeSpawnEntity(SpawnArea spawnArea)
    {
        System.Random rnd = new System.Random();
        SortedSet<Tile> tiles = spawnArea.GetArea().GetAllTiles();
        IEnumerable<Tile> tilesWithEntities = entitiesPosition.Keys.AsEnumerable<Tile>();

        tiles.IntersectWith(tilesWithEntities);
        Tile despawningTile = tiles.ElementAt(rnd.Next((tiles.Count() - 1)));

        GameObject despawningEntity =  entitiesPosition.Where(a => a.Key == despawningTile).FirstOrDefault().Value;

        //Eintrag aus dem Dictionary entfernen
        entitiesPosition.Remove(despawningTile);

        //eigentliches GameObject zerstören
        Destroy(despawningEntity);    
    }

    private void UpdateEntitiesForAllAreas()
    {
        foreach (SpawnArea spawnArea in spawnAreas)
        {
            while (spawnArea.SpawnNewEntityAllowed())
                SpawnEntity(spawnArea);
            while (spawnArea.DeSpawnEntityRequired())
                DeSpawnEntity(spawnArea);
        }
    }

    public void Notify()
    {
        CalculateSpawnAreas();
        UpdateEntitiesForAllAreas();
        Debug.Log("Notified Spawner");
    }
    #endregion
}

