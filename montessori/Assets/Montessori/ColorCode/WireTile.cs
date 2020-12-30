using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Montessori.ColorCode
{
    public class WireTile : MonoBehaviour
    {
        [SerializeField]
        private Material materialRed = null;
        [SerializeField]
        private Material materialGreen = null;

        public void SetColor(Color color)
        {
            Material material = null;
            if (color.Equals(Color.GREEN))
                material = materialGreen;
            else if (color.Equals(Color.RED))
                material = materialRed;
            else
            {
                Destroy(this);
                return;
            }

            //fetch each child and apply material
            for (int i = 0; i < transform.childCount; i++)
            {
                MeshRenderer meshRenderer = transform
                    .GetChild(i)
                    .gameObject
                    .GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                    meshRenderer.material = material;
            }
        }
    }
}

