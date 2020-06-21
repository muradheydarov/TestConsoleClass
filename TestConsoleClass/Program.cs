using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestConsoleClass
{
    class Program
    {
        static void Main(string[] args)
        {


            int[] arr1 = new int[] { 0 };
            int[] arr2 = new int[] { 1 };

            int m = 0;
            int n = 1;

            Merge(arr1, m, arr2, n);

            foreach (var item in arr1)
            {
                Console.WriteLine(item);
            }
        }


        static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int i = 0;
            int k = 0;

            while (n != 0)
            {
                if (nums2[k] <= nums1[i])
                {
                    shiftRight(nums1, i);

                    nums1[i] = nums2[k];

                    k++;
                    n--;
                }
                else if (nums2[k] > nums1[i + 1])
                {
                    shiftRight(nums1, i + 1);

                    nums1[i + 2] = nums2[k];

                    k++;
                    n--;
                }
                else if (true)
                {

                }
                
                i++;
            }
        }

        static void shiftRight(int[] arr, int index)
        {
            for (int i = arr.Length - 2; i >= index; i--)
            {
                arr[i + 1] = arr[i];
            }
        }
    }
}
