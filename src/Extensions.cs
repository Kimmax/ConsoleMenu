using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuernberger
{
    public static class Extensions
    {
        public static T[] Populate<T>(this T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }

            return arr;
        }
    }
}
