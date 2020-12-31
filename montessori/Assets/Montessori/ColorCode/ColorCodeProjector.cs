using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;

namespace Assets.Montessori.ColorCode
{
    public class ColorCodeProjector : MonoBehaviour, ISubscriber
    {
        [SerializeField]
        private GameObject wireTilePrefab = null;
        [SerializeField]
        private float wireTileHeight = 512;
        [SerializeField]
        private AreaCalculator areaCalculator = null;
        private IAreaCalculator i_areaCalculator;
        private IPublisher publisher;
        private IColorCodeSource source = null;

        // Start is called before the first frame update
        void Start()
        {
            publisher = areaCalculator;
            i_areaCalculator = areaCalculator;
            publisher.Attach(this);
        }

        public void Notify()
        {
            //TODO: where is the source?
            //get colorCodeArray from source
            Color[,] colorArray = source.GetColorArray();
            
            //get grid with Tiles and their positions from areaCalculator
            HueterDesWaldes.AreaCalculation.Grid grid = i_areaCalculator.GetGrid();

            //instantiate wireTiles and apply colors
            int rows = colorArray.GetLength(0);
            int collumns = colorArray.GetLength(1);
            for(int r = 0; r < rows; r++)
            {
                for(int c = 0; c < collumns; c++)
                {
                    //get color of position
                    Color color = colorArray[r, c];
                    //get tile position in grid
                    Vector3 pos = grid.TileAt(r, c).WorldPos;
                    pos.y = wireTileHeight;
                    //instantiate WireTile
                    WireTile wireTile = Instantiate(wireTilePrefab, pos, Quaternion.identity, transform)
                        .GetComponent<WireTile>();
                    //set correct material
                    wireTile.SetColor(color);
                }
            }
        }
    }
}
