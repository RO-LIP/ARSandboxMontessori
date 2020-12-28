using System.IO;
using UnityEngine;

namespace Assets.Montessori.BitmapConversion
{
    public class BitmapConverter
    {
        public int[,] ConvertTextureToBitmap(Texture2D tex)
        {
            int rows = tex.height;
            int cols = tex.width;

            Color[] pixelArray = tex.GetPixels();
            
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
           
            return ConvertArrayInto2DArray(bitArray, rows, cols);
        }

        public int[,] ResizeBitmap(int[,] bitmap, int gridWidth, int gridHeight)
        {
            int[,] resizedBitmap = new int[gridHeight, gridWidth];

            int tileWidth = bitmap.GetLength(0) / gridWidth;
            int tileHeight = bitmap.GetLength(1) / gridHeight;

            int bitPosX = 0;
            int bitPosY;

            // Iterate through the tiles
            for (int i = 0; i < bitmap.GetLength(1); i += tileHeight)
            {
                bitPosY = 0;
                for (int j = 0; j < bitmap.GetLength(0); j += tileWidth)
                {
                    // calculate bit value of the particular tile and set the corresponding bit in the resized bitmap
                    // Algorithm: if at least one bit within the tile is "1", the "resized-bit" should also be "1"
                    for (int a = 0; a < tileHeight; a++)
                    {
                        for (int b = 0; b < tileWidth; b++)
                        {
                            if (bitmap[i + a, j + b] == 1)
                            {
                                resizedBitmap[bitPosX, bitPosY] = 1;
                                break;
                            }
                            else
                            {
                                resizedBitmap[bitPosX, bitPosY] = 0;
                            }
                        }
                    }
                    bitPosY++;
                }
                bitPosX++;
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
    }
}
