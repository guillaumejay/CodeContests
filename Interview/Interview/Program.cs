using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] toSort = new int[] {8, 6, 5,11, 4, 3, 2,9, 1, 7};
        // MergeSort(toSort, 0, toSort.Length - 1 );
            QuickSort(toSort, 0, toSort.Length - 1);
            DisplayTable(toSort);
        }

        private static void QuickSort(int[] t,int iStart,int iEnd)
        {
            int pivot = t[(iStart + iEnd)/2];
            int iL = iStart, iR = iEnd;
            while (iL <= iR)
            {
                while (t[iL] < pivot)
                {
                    iL++;
                }
                while (t[iR] > pivot)
                { 
                    iR--;
                }
                if (iL <= iR)
                {
                    swap(t,iL++,iR--);
                }
            }
            if (iStart<iR)
                QuickSort(t,iStart,iR);
            if (iL < iEnd)
                QuickSort(t, iL, iEnd);


        }

        public static void swap(int[] t, int a, int b)
        {
            int te = t[a];
            t[a] = t[b];
            t[b] = te;
        }
        #region MergeSort
        private static void MergeSort(int[] current, int iStart, int iEnd)
        {
            if (iEnd > iStart)
            {
                int iMid = (iStart + iEnd)/2;
                 MergeSort(current, iStart, iMid);
                MergeSort(current, iMid+1, iEnd);
                 MergeArrays( current,iStart,iMid,iEnd);
                Console.WriteLine(iStart + " " + iEnd + ":");
                DisplayTable(current);
            }
        }

        private static void MergeArrays(int[] t, int iStart, int iMid, int iEnd)
        {
            int i0 = iStart,i1= iMid+1;
            var temp = new int[iEnd-iStart+1];

            for (int i = iStart; i <= iEnd; i++)
            {
                if (i0<=iMid && (i1>iEnd || t[i0]<=t[i1]) )
                {
                    temp[i-iStart]=t[i0++];
                }
                else
                {
                  temp[i-iStart]=t[i1++];
                }
            }
            for (int i = iStart; i <= iEnd; i++)
            {
                t[i] = temp[i-iStart];
            }
            
        }


    

        #endregion

        private static void DisplayTable(int [] myArray)
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.Write(myArray[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
