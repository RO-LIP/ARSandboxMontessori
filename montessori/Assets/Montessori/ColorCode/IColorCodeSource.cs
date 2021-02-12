using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Montessori.ColorCode
{
    public enum Color { UNDEFINED, GREEN, RED }
    
    public interface IColorCodeSource
    {
        /// <summary>
        /// Return array of type Assets.Montessori.ColorCode.Color.
        /// Array dimensions are [rows, collumns].
        /// Origin ([0,0]) represents the bottom left corner.
        /// </summary>
        Color[,] GetColorArray();
    }
}
