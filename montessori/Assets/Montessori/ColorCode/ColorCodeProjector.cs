using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;
using System.Collections.Generic;

namespace Assets.Montessori.ColorCode
{
    public class ColorCodeProjector : MonoBehaviour, ISubscriber
    {
        [SerializeField]
        private GameObject wireTilePrefab = null;
        [SerializeField]
        private float wireTileHeight = 512;
        public IAreaCalculator i_areaCalculator { private get; set; }
        public IPublisher publisher { private get; set; }
        public IColorCodeSource source { private get; set; }
        private List<GameObject> tiles = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            publisher.Attach(this);
        }

        public void Notify(bool result = false)
        {
            //destroy previous tiles
            foreach (GameObject tile in tiles)
                Destroy(tile);

            //get colorCodeArray from source
            Color[,] colorArray = source.GetColorArray();

            //get grid with Tiles and their positions from areaCalculator
            HueterDesWaldes.AreaCalculation.Grid grid = i_areaCalculator.GetGrid();

            //instantiate wireTiles and apply colors
            int rows = colorArray.GetLength(0);
            int collumns = colorArray.GetLength(1);
            int u_cnt = 0;
            int g_cnt = 0;
            int r_cnt = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < collumns; c++)
                {
                    //get color of position
                    Color color = colorArray[r, c];

                    if (color.Equals(Color.UNDEFINED))
                        u_cnt++;
                    else if (color.Equals(Color.GREEN))
                        g_cnt++;
                    else if (color.Equals(Color.RED))
                        r_cnt++;

                    //if (color.Equals(Color.UNDEFINED))
                    //    continue;
                    //else
                    //{
                    //get tile position in grid
                    Vector3 pos = grid.TileAt(r, c).WorldPos;
                    pos.y = wireTileHeight;
                    //instantiate WireTile
                    WireTile wireTile = Instantiate(wireTilePrefab, pos, Quaternion.identity, transform)
                        .GetComponent<WireTile>();
                    tiles.Add(wireTile.gameObject);
                    //set correct material
                    wireTile.SetColor(color);
                    //set size
                    float scaling = i_areaCalculator.GetGrid().TileLengthX;
                    wireTile.transform.localScale = Vector3.one * scaling * 0.8f;
                    //}
                }
            }
            Debug.Log("Color-Count: UNDEFINED=" + u_cnt + " GREEN=" + g_cnt + " RED=" + r_cnt);
        }
    }
}
