using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class TriFibonacci
    {
        public int Complete(int[] test)
        {
            int index = Array.IndexOf(test, -1);
            TriFiboSeries series = new TriFiboSeries(test);
            return (series.CheckTriFiboSeries(test))?series.GetNumberAtIndex(index):-1;
        }

        #region Testing code Do not change
        //public static void Main(String[] args)
        //{
        //    String input = Console.ReadLine();
        //    TriFibonacci triFibonacci = new TriFibonacci();
        //    do
        //    {
        //        string[] values = input.Split(',');
        //        int[] numbers = Array.ConvertAll<string, int>(values, delegate (string s) { return Int32.Parse(s); });
        //        int result = triFibonacci.Complete(numbers);
        //        Console.WriteLine(result);
        //        input = Console.ReadLine();
        //    } while (input != "-1");
        //}
        #endregion
    }

    public class TriFiboSeries
    {
        int[] baseSeries= new int[3];

        public TriFiboSeries(int[] fiboseries)
        {
            
            for(int i =0;i<3;i++)
                if(fiboseries[i]==-1)
                    fiboseries[i]= fiboseries[3]- fiboseries[0] - fiboseries[1] - fiboseries[2] -1;
            Array.Copy(fiboseries, baseSeries, 3);
        }

        public bool CheckTriFiboSeries(int[] fiboseries)
        {
            for(int i = 0; i < fiboseries.Length; i++)
            {
                if (fiboseries[i] == GetNumberAtIndex(i) || fiboseries[i]==-1)
                    continue;
                return false;
            }
            return true;
        }

        public int GetNumberAtIndex(int index)
        {
            if (index < 3)
                return (baseSeries[index]>0)?baseSeries[index]:-1;
            int i = 3;
            int temp=0,temp1= baseSeries[0], temp2 = baseSeries[1], temp3= baseSeries[2];
            if (index == 0 || index == 1 || index == 2)
            {
                return baseSeries[index];
            }

            while (i <= index)
            {
                temp = temp3;
                temp3 = temp + temp1 + temp2;
                temp1 = temp2;
                temp2 = temp;
                i++;
            }
            return (temp3>0)?temp3:-1;
        }
    }
}