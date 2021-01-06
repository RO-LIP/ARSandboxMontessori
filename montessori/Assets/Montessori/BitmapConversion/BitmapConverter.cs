using System.IO;
using UnityEngine;
using Assets.HueterDesWaldes.AreaCalculation;

namespace Assets.Montessori.BitmapConversion
{
    public class BitmapConverter : MonoBehaviour, ISubscriber
    {
        public AreaCalculator areaCalculator { private get; set; }
        private int gridRows;
        private int gridCols;
        private int[,] convertedBitmap;
        private Texture2D currentTexture;

        private SpriteRenderer spriteRend;
        private Object[] sprites;
        private Sprite currentSprite;

        void Start()
        {
            spriteRend = gameObject.GetComponent<SpriteRenderer>();
            sprites = Resources.LoadAll("Textures", typeof(Sprite));
            ConvertTextureToBitmap();
        }

        /*
        void Update()
        {
        }*/

        private void ShowRandomExercise()
        {            
            spriteRend.sprite = (Sprite)sprites[Random.Range(0, sprites.Length)];
            currentSprite = spriteRend.sprite;
            spriteRend.color = new Color(0f, 0f, 0f, 0.7f);
            Debug.Log("Showing exercise: " + spriteRend.sprite.name);
            currentTexture = currentSprite.texture;
        }
        private void ConvertTextureToBitmap()
        {
            ShowRandomExercise();
            Color[] pixelArray = currentTexture.GetPixels();

            int[] bitArray = new int[pixelArray.Length];

            for (int pix = 0; pix < pixelArray.Length - 1; pix++)
            {
                if (pixelArray[pix] == Color.black)
                {
                    bitArray[pix] = 1;
                }
                else
                {
                    bitArray[pix] = 0;
                }
            }
            int[,] originalBitmap = ConvertArrayInto2DArray(bitArray, currentTexture.height, currentTexture.width);            
            convertedBitmap = ResizeBitmap(originalBitmap);

            // For testing purposes only
            // SaveBitmapAsCsvFile(originalBitmap, "C:/Users/hello/Desktop/bitmap1.csv");
            // SaveBitmapAsCsvFile(convertedBitmap, "C:/Users/hello/Desktop/resizedBitmap1.csv");            
        }

        public int[,] GetBitmapConverted()
        {
            return convertedBitmap;
        }        

        public int[,] ConvertArrayInto2DArray(int[] array, int rows, int cols)
        {
            int index = 0;
            int[,] array2D = new int[cols, rows];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    array2D[y, x] = array[index];
                    index++;
                }
            }
            return array2D;
        }

        private int[,] ResizeBitmap(int[,] bitmap)
        {
            Assets.HueterDesWaldes.AreaCalculation.Grid grid = areaCalculator.GetGrid();

            gridCols = grid.ResolutionZ;
            gridRows = grid.ResolutionX;

            int[,] resizedBitmap = new int[gridRows, gridCols];
            int[,] tile = new int[bitmap.GetLength(0) / gridRows, bitmap.GetLength(1) / gridCols];

            int rowPosition = 0;
            int colPosition = 0;

            //Resize all Rows
            for (int j = 0; j < bitmap.GetLength(0) - tile.GetLength(0); j += tile.GetLength(0))
            {
                //Resize One Row
                for (int i = 0; i < bitmap.GetLength(1) - tile.GetLength(1); i += tile.GetLength(1))
                {
                    //Resize One Tile
                    resizedBitmap[rowPosition, colPosition] = ResizeTile(bitmap, j, i, tile.GetLength(0), tile.GetLength(1));
                    colPosition++;
                    if (colPosition == gridCols)
                    {
                        break;
                    }
                }
                colPosition = 0;
                rowPosition++;
                if (rowPosition == gridRows)
                {
                    break;
                }
            }
            return resizedBitmap;
        }

        private int ResizeTile(int[,] bitmap, int rowPosition, int colPosition, int tileRows, int tileCols)
        {
            for (int i = 0; i < tileRows && i < bitmap.GetLength(0); i++)
            {
                for (int j = 0; j < tileCols && j < bitmap.GetLength(1); j++)
                {
                    if (bitmap[rowPosition + i, colPosition + j] == 1)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public void SaveBitmapAsCsvFile(int[,] bitmap, string pathName)
        {
            int rows = bitmap.GetLength(0);
            int cols = bitmap.GetLength(1);

            StreamWriter file = new StreamWriter(pathName);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    file.Write(bitmap[row, col]);
                }
                file.Write("\n");
            }
        }

        public void Notify(bool result = false)
        {
            if (result)
            {
                ConvertTextureToBitmap();
            }        
            Debug.Log("Notified BitmapConverter");
        }        
    }
}
    
