using UnityEngine;

namespace DllSky.StarterKITv2.Tools.Arrays
{
    public static class ToolArrays
    {
        /// <summary>
        /// Возвращает сквозной индекс для элемента массива
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_xCount"></param>
        /// <returns></returns>
        public static int GetSingleIndex(int x, int y, int xCount)
        {
            return x + y * xCount;
        }

        /// <summary>
        /// Возвращает сквозной индекс для элемента массива
        /// </summary>
        /// <param name="_coord"></param>
        /// <param name="_xCount"></param>
        /// <returns></returns>
        public static int GetSingleIndex(Vector2Int coord, int xCount)
        {
            return coord.x + coord.y * xCount;
        }
    }
}
