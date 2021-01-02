using System.IO;
using UnityEngine;

namespace Assets.Montessori.BitmapConversion
{
    public class BitmapConverter : MonoBehaviour, ISubscriber
    {  
        private int gridRows = 64;
        private int gridCols = 128;
        private int[,] convertedBitmap;
        private Object[] sourceTextures;
        private Texture2D currentTexture;

        void Start()
        {
            sourceTextures = Resources.LoadAll("Textures", typeof(Texture2D));
            currentTexture = (Texture2D)sourceTextures[Random.Range(0, sourceTextures.Length)];
        }

        /*
        void Update()
        {

        }*/

        private void ConvertTextureToBitmap()
        {
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
            convertedBitmap = ResizeBitmap(originalBitmap, gridRows, gridCols);
        }

        public int[,] GetBitmapConverted()
        {
            return convertedBitmap;
        }

        public int[,] ResizeBitmap(int[,] bitmap, int gridRows, int gridCols)
        {
            int[,] resizedBitmap = new int[gridRows, gridCols];

            int tileCols = bitmap.GetLength(1) / gridCols;
            int tileRows = bitmap.GetLength(0) / gridRows;

            int newColPosition;
            int newRowPosition = 0;

            // Iterate through the tiles
            for (int i = 0; i < bitmap.GetLength(1) - tileRows; i += tileRows)
            {
                newColPosition = 0;
                for (int j = 0; j < bitmap.GetLength(0) - tileCols; j += tileCols)
                {
                    // calculate bit value of the particular tile and set the corresponding bit in the resized bitmap
                    // Algorithm: if at least one bit within the tile is "1", the "resized-bit" should also be "1"
                    if (newColPosition >= gridCols)
                    {
                        newColPosition = gridCols - 1;
                    }
                    if (newRowPosition >= gridRows)
                    {
                        newRowPosition = gridRows - 1;
                    }
                    for (int a = 0; a < tileRows; a++)
                    {
                        for (int b = 0; b < tileCols; b++)
                        {
                            if (i + a >= bitmap.GetLength(0))
                            {
                                break;
                            }
                            if (bitmap[i + a, j + b] == 1)
                            {
                                resizedBitmap[newRowPosition, newColPosition] = 1;
                                break;
                            }
                            else
                            {
                                resizedBitmap[newRowPosition, newColPosition] = 0;
                            }
                        }
                    }
                    newColPosition++;
                }
                newRowPosition++;
            }
            return resizedBitmap;
        }

        public int[,] ConvertArrayInto2DArray(int[] array, int rows, int cols)
        {
            int index = 0;
            int[,] array2D = new int[rows, cols];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    array2D[x, y] = array[index];
                    index++;
                }
            }
            return array2D;
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

        public void Notify()
        {
            ConvertTextureToBitmap();
            Debug.Log("Notified BitmapConverter");
        }
    }
}
    
