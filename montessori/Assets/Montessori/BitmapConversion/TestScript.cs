using UnityEngine;
using UnityEngine.UI;

namespace Assets.Montessori.BitmapConversion
{
    public class TestScript : MonoBehaviour
    {
        BitmapConverter converter = new BitmapConverter();

        private Object[] sourceTextures;
        private Texture2D currentTexture;
        int gridWidth = 128; // should use the method GetResolutionX bzw. Z
        int gridHeight = 64;
        

        void Start()
        {
            // Load all textures from Resources folder into array of textures
            sourceTextures = Resources.LoadAll("Textures", typeof(Texture2D));

            // Display a random exercise(texture) on screen/sandbox
            currentTexture = ShowRandomExercise();

            // Create bitmap from Texture
            int[,] bitmap = converter.ConvertTextureToBitmap(currentTexture);
            
            // Resize bitmap according to Grid dimensions
            int[,] resizedBitmap = converter.ResizeBitmap(bitmap, gridWidth, gridHeight);

            // for local test purposes only
            //converter.SaveBitmapAsCsvFile(bitmap, "C:/Users/hello/Desktop/originalBitmap.csv");
            //converter.SaveBitmapAsCsvFile(resizedBitmap, "C:/Users/hello/Desktop/resizedBitmap.csv");
          
        }

        /*
        void Update()
        {

        }*/

        private Texture2D ShowRandomExercise()
        {
            Texture2D texture = (Texture2D)sourceTextures[Random.Range(0, sourceTextures.Length)];
            GameObject rawImage = GameObject.Find("RawImage");
            rawImage.GetComponent<RawImage>().texture = texture;
            return texture;
        }
    }
}
